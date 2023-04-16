using Lexicom.Wpf.Amenities.Extensions;
using System.Windows;

namespace Lexicom.Wpf.Amenities.Themes;
public class WpfThemeProvider : IThemeProvider
{
    private readonly Application _application;

    /// <exception cref="ArgumentNullException"/>
    public WpfThemeProvider(Application application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _application = application;
    }

    public Task<IReadOnlyList<string>> GetThemesAsync()
    {
        IReadOnlyList<ThemeResourceDictionary> themeResourceDictionaries = _application.GetThemeResourceDictionaries();

        var themes = themeResourceDictionaries
            .Select(trd => trd.Theme)
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Select(t => t!)
            .ToList();

        return Task.FromResult<IReadOnlyList<string>>(themes);
    }

    public async Task<string?> GetAppliedThemeAsync()
    {
        //because in wpf resources are applied in the order
        //they occur, we know the currently applied theme
        //is whatever theme is last in the list
        IReadOnlyList<string> themes = await GetThemesAsync();

        return themes.LastOrDefault();
    }
}
