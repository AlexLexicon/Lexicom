using Microsoft.Extensions.Options;

namespace Lexicom.Supports.UnitTesting.Options;
public class TestOptionsSnapshot<T>(T value) : IOptionsSnapshot<T> where T : class
{
    public T Value { get; } = value;

    public T Get(string? name) => Value;
}
