namespace Lexicom.Wpf.Amenities.Exceptions;
public class ThemeDoesNotExistException : Exception
{
    public ThemeDoesNotExistException(string? theme) : base($"The theme '{theme ?? "null"}' does not exist.")
    {
    }
}
