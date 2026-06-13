namespace Lexicom.Validation.Amenities;

public interface IDefaultMessagePropertyValidator
{
    string Name { get; }
    string DefaultMessageTemplate { get; }
    string GetDefaultMessageTemplate(string? errorCode);
    string Localized(string? errorCode, string fallbackKey);
}
