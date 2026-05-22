using Lexicom.Wpf.Amenities.Exceptions;

namespace Lexicom.Wpf.Amenities.Themes;
public interface IThemeApplicator
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="ThemeDoesNotExistException"/>
    Task ApplyAsync(string theme);
}
