using Lexicom.Wpf.Amenities.Themes;
using System.Windows;

namespace Lexicom.Wpf.Amenities.Extensions;
public static class ApplicationExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static IReadOnlyList<ThemeResourceDictionary> GetThemeResourceDictionaries(this Application application)
    {
        ArgumentNullException.ThrowIfNull(application);

        var themeResourceDictionaries = new List<ThemeResourceDictionary>();
        foreach (ResourceDictionary applicationResourcesMergedDictionaries in application.Resources.MergedDictionaries)
        {
            if (applicationResourcesMergedDictionaries is ThemeResourceDictionary resourceDictionary)
            {
                themeResourceDictionaries.Add(resourceDictionary);
            }
        }

        return themeResourceDictionaries;
    }
}
