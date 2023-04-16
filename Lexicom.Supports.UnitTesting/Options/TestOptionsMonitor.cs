using Microsoft.Extensions.Options;

namespace Lexicom.Supports.UnitTesting.Options;
public class TestOptionsMonitor<T> : IOptionsMonitor<T>
{
    public TestOptionsMonitor(T currentValue)
    {
        CurrentValue = currentValue;
    }

    public T CurrentValue { get; }

    public T Get(string? name) => CurrentValue;
    public IDisposable? OnChange(Action<T, string?> listener) => new OnChangeDisposable(listener);

    public class OnChangeDisposable : IDisposable
    {
        public OnChangeDisposable(Action<T, string?> listener)
        {
            Listenser = listener;
        }

        public Action<T, string?> Listenser { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
