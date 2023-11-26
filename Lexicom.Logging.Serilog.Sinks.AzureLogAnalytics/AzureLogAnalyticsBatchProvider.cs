using Serilog.Debugging;
using Serilog.Events;
using System.Collections.Concurrent;

namespace Lexicom.Logging.Serilog.Sinks.AzureLogAnalytics;
internal abstract class AzureLogAnalyticsBatchProvider : IDisposable
{
    private const int BUFFER_SIZE_MAXIMUM = 100_000;
    private const int BATCH_SIZE_MAXIMUM = 1_000;

    private readonly int _maxBufferSize;
    private readonly int _batchSize;
    private readonly ConcurrentQueue<LogEvent> _logEventBatch;
    private readonly BlockingCollection<IList<LogEvent>> _batchEventsCollection;
    private readonly BlockingCollection<LogEvent> _eventsCollection;
    private readonly TimeSpan _timerThresholdSpan;
    private readonly TimeSpan _transientThresholdSpan;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly AutoResetEvent _timerResetEvent;
    private readonly SemaphoreSlim _semaphoreSlim;
    private readonly Task _timerTask;
    private readonly Task _batchTask;
    private readonly Task _eventPumpTask;

    private int _numMessages;
    private bool _canStop;

    protected AzureLogAnalyticsBatchProvider(
        int batchSize = 100, 
        int maxBufferSize = 25_000)
    {
        _maxBufferSize = Math.Min(Math.Max(5_000, maxBufferSize), BUFFER_SIZE_MAXIMUM);
        _batchSize = Math.Min(Math.Max(batchSize, 1), BATCH_SIZE_MAXIMUM);

        _logEventBatch = new ConcurrentQueue<LogEvent>();
        _batchEventsCollection = [];
        _eventsCollection = new BlockingCollection<LogEvent>(maxBufferSize);

        _timerThresholdSpan = TimeSpan.FromSeconds(10);
        _transientThresholdSpan = TimeSpan.FromSeconds(5);

        _cancellationTokenSource = new CancellationTokenSource();
        _timerResetEvent = new AutoResetEvent(false);
        _semaphoreSlim = new SemaphoreSlim(1, 1);

        _batchTask = Task.Factory.StartNew(PumpAsync, TaskCreationOptions.LongRunning);
        _timerTask = Task.Factory.StartNew(TimerPump, TaskCreationOptions.LongRunning);
        _eventPumpTask = Task.Factory.StartNew(EventPump, TaskCreationOptions.LongRunning);
    }

    protected abstract Task<bool> WriteLogEventAsync(ICollection<LogEvent> logEventsBatch);

    private async Task PumpAsync()
    {
        try
        {
            while (!_batchEventsCollection.IsCompleted)
            {
                IList<LogEvent> logEvents = _batchEventsCollection.Take(_cancellationTokenSource.Token);

                SelfLog.WriteLine("Sending AzureLogAnalytics logs batch with '{0}' events.", logEvents.Count);

                bool writeLogEventResult = await WriteLogEventAsync(logEvents).ConfigureAwait(false);

                if (writeLogEventResult)
                {
                    SelfLog.WriteLine("Sending AzureLogAnalytics logs batch was successful.");
                    Interlocked.Add(ref _numMessages, -1 * logEvents.Count);
                }
                else
                {
                    SelfLog.WriteLine("Sending AzureLogAnalytics logs batch was unsuccessful.");
                    SelfLog.WriteLine("Retrying after '{0}' seconds.", _transientThresholdSpan.TotalSeconds);

                    await Task.Delay(_transientThresholdSpan).ConfigureAwait(false);

                    if (!_batchEventsCollection.IsAddingCompleted)
                    {
                        _batchEventsCollection.Add(logEvents);
                    }
                }

                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
            }
        }
        catch (InvalidOperationException) { }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to pump the log events because '{0}' at {1}.", ex, ex.StackTrace);
        }
    }

    private void TimerPump()
    {
        while (!_canStop)
        {
            _timerResetEvent.WaitOne(_timerThresholdSpan);

            FlushLogEventBatch();
        }
    }

    private void EventPump()
    {
        try
        {
            while (!_eventsCollection.IsCompleted)
            {
                LogEvent logEvent = _eventsCollection.Take(_cancellationTokenSource.Token);

                _logEventBatch.Enqueue(logEvent);

                if (_logEventBatch.Count >= _batchSize)
                {
                    FlushLogEventBatch();
                }
            }
        }
        catch (InvalidOperationException) { }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to event pump because '{0}' at {1}.", ex, ex.StackTrace);
        }
    }

    private void FlushLogEventBatch()
    {
        try
        {
            _semaphoreSlim.Wait(_cancellationTokenSource.Token);

            if (_logEventBatch.IsEmpty)
            {
                return;
            }

            int logEventBatchSize = _logEventBatch.Count >= _batchSize ? _batchSize : _logEventBatch.Count;

            var logEventList = new List<LogEvent>();
            for (int i = 0; i < logEventBatchSize; i++)
            {
                if (_logEventBatch.TryDequeue(out LogEvent? logEvent))
                {
                    logEventList.Add(logEvent);
                }
            }

            if (!_batchEventsCollection.IsAddingCompleted)
            {
                _batchEventsCollection.Add(logEventList);
            }
        }
        catch (InvalidOperationException) { }
        catch (OperationCanceledException) { }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to flush the log events because '{0}' at {1}.", ex, ex.StackTrace);
        }
        finally
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                _semaphoreSlim.Release();
            }
        }
    }

    /// <exception cref="ArgumentNullException"/>
    protected void PushEvent(LogEvent logEvent)
    {
        ArgumentNullException.ThrowIfNull(logEvent);

        if (_numMessages <= _maxBufferSize && !_eventsCollection.IsAddingCompleted)
        {
            _eventsCollection.Add(logEvent);

            Interlocked.Increment(ref _numMessages);
        }
    }

    private bool _isAlreadyDisposed;
    public void Dispose()
    {
        if (_isAlreadyDisposed)
        {
            return;
        }

        FlushAndCloseEventHandlers();

        _semaphoreSlim.Dispose();

        SelfLog.WriteLine("AzureLogAnalytics sink disposed successfully.");

        _isAlreadyDisposed = true;
    }

    private void FlushAndCloseEventHandlers()
    {
        try
        {
            SelfLog.WriteLine("Halting AzureLogAnalytics sink.");

            _canStop = true;
            _timerResetEvent.Set();
            _eventsCollection.CompleteAdding();

            // Flush events collection
            while (!_eventsCollection.IsCompleted)
            {
                LogEvent logEvent = _eventsCollection.Take();

                _logEventBatch.Enqueue(logEvent);

                if (_logEventBatch.Count >= _batchSize)
                {
                    FlushLogEventBatch();
                }
            }

            FlushLogEventBatch();

            _batchEventsCollection.CompleteAdding();

            // request cancellation of all tasks
            _cancellationTokenSource.Cancel();

            // Flush events batch
            while (!_batchEventsCollection.IsCompleted)
            {
                IList<LogEvent> eventBatch = _batchEventsCollection.Take();

                WriteLogEventAsync(eventBatch).GetAwaiter().GetResult();
            }

            Task.WaitAll([_eventPumpTask, _batchTask, _timerTask], TimeSpan.FromSeconds(60));
        }
        catch (Exception ex)
        {
            SelfLog.WriteLine("There was a unexpected error while trying to flush and close the event handlers because '{0}' at {1}.", ex, ex.StackTrace);
        }
    }
}
