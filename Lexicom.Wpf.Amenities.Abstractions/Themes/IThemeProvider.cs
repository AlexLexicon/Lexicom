namespace Lexicom.Wpf.Amenities.Themes;
public interface IThemeProvider
{
    Task<IReadOnlyList<string>> GetThemesAsync();
    Task<string?> GetAppliedThemeAsync();
}
