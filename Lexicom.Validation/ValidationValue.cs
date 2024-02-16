namespace Lexicom.Validation;
public class ValidationValue<T>
{
    public ValidationValue(T value)
    {
        Value = value;
    }

    public T Value { get; }

    public override string? ToString()
    {
        return Value?.ToString();
    }
}
