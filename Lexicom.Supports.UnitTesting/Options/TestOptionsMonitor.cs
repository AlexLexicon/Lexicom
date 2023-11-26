using Microsoft.Extensions.Options;

namespace Lexicom.Supports.UnitTesting.Options;
public class TestOptionsMonitor<T>(T currentValue) : IOptionsMonitor<T>
{
    public T CurrentValue { get; } = currentValue;

    public T Get(string? name) => CurrentValue;
    public IDisposable? OnChange(Action<T, string?> listener) => new OnChangeDisposable(listener);

    public class OnChangeDisposable : IDisposable
    {
        /// <exception cref="ArgumentNullException"/>
        public OnChangeDisposable(Action<T, string?> listener)
        {
            ArgumentNullException.ThrowIfNull(listener);

            Listenser = listener;
        }

        public Action<T, string?> Listenser { get; }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
