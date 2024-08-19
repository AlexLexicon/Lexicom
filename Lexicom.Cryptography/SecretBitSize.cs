namespace Lexicom.Cryptography;
public class SecretBitSize
{
    public required int Value { get; init; }
    public required IReadOnlySet<int> AllowedSizes { get; init; }

    private bool? _isValid;
    public bool IsValid => _isValid ??= AllowedSizes.Any(s => s == Value);
}
