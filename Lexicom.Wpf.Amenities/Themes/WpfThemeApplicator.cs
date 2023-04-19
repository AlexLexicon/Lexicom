using Lexicom.Wpf.Amenities.Exceptions;
using Lexicom.Wpf.Amenities.Extensions;
using System.Windows;

namespace Lexicom.Wpf.Amenities.Themes;
public class WpfThemeApplicator : IThemeApplicator
{
    private readonly Application _application;

    /// <exception cref="ArgumentNullException"/>
    public WpfThemeApplicator(Application application)
    {
        ArgumentNullException.ThrowIfNull(application);

        _application = application;
    }

    /// <exception cref="ThemeDoesNotExistException"/>
    public Task ApplyAsync(string theme)
    {
        IReadOnlyList<ThemeResourceDictionary> themeResourceDictionaries = _application.GetThemeResourceDictionaries();

        ThemeResourceDictionary? themeResourceDictionary = themeResourceDictionaries.FirstOrDefault(trd => string.Equals(trd.Theme, theme, StringComparison.OrdinalIgnoreCase));

        if (themeResourceDictionary is null)
        {
            throw new ThemeDoesNotExistException(theme);
        }

        //by removing and then adding the dictionary
        //we are moving it to the end of the list 
        //which is the one that will be used
        _application.Resources.MergedDictionaries.Remove(themeResourceDictionary);
        _application.Resources.MergedDictionaries.Add(themeResourceDictionary);

        return Task.CompletedTask;
    }
}
