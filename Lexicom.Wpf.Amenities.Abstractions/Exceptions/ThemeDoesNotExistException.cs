namespace Lexicom.Wpf.Amenities.Exceptions;
public class ThemeDoesNotExistException(string? theme) : Exception($"The theme '{theme ?? "null"}' does not exist.")
{
}
