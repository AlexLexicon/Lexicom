namespace Lexicom.Validation;
public class ValidationValue<T>
{
    public required T Value { get; init; }

    public override string? ToString()
    {
        return Value?.ToString();
    }
}
