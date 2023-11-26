using Lexicom.Wpf.Markup.Exceptions;
using System.IO;
using System.Reflection;
using System.Windows.Markup;
using System.Windows;

namespace Lexicom.Wpf.Markup;
//based on: https://stackoverflow.com/questions/910814/loading-xaml-at-runtime
public class DynamicXamlLoader : MarkupExtension
{
    protected readonly List<Assembly> _assemblies = [];

    public DynamicXamlLoader()
    {
        IsSuppressingExceptions = true;

        Assembly? entryAssembly = Assembly.GetEntryAssembly();
        if (entryAssembly is not null)
        {
            _assemblies.Add(entryAssembly);
        }

        Assembly? executingAssembly = Assembly.GetExecutingAssembly();
        if (executingAssembly is not null)
        {
            _assemblies.Add(executingAssembly);
        }
    }

    public string? XamlFileName { get; set; }
    public bool IsSuppressingExceptions { get; set; }

    /// <exception cref="ArgumentNullException"/>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(XamlFileName);

        if (serviceProvider.GetService(typeof(IProvideValueTarget)) is not IProvideValueTarget provideValue || provideValue.TargetObject is null || provideValue.TargetObject is not UIElement)
        {
            return null;
        }

        return LoadFile(XamlFileName);
    }

    /// <exception cref="ArgumentNullException"/>
    protected virtual object? LoadFile(string xamlFileName)
    {
        ArgumentNullException.ThrowIfNull(xamlFileName);

        foreach (Assembly assembly in _assemblies)
        {
            if (TryLoadFromAssembly(assembly, xamlFileName, out object? result))
            {
                return result;
            }
        }

        if (!IsSuppressingExceptions)
        {
            throw new XamlFileNotFoundException();
        }

        return null;
    }

    /// <exception cref="ArgumentNullException"/>
    protected virtual bool TryLoadFromAssembly(Assembly assembly, string xamlFileName, out object? result)
    {
        ArgumentNullException.ThrowIfNull(assembly);
        ArgumentNullException.ThrowIfNull(xamlFileName);

        result = null;

        string[] manifestResourceNames = assembly.GetManifestResourceNames();

        string fullXamlfileName = xamlFileName;

        if (fullXamlfileName.IndexOf('.') < 0)
        {
            fullXamlfileName = $"{xamlFileName}.xaml";
        }

        string? manifestResourceName = manifestResourceNames.FirstOrDefault(n => n.EndsWith($".{fullXamlfileName}"));

        if (manifestResourceName is null)
        {
            return false;
        }

        using Stream? xamlStream = assembly.GetManifestResourceStream(manifestResourceName);

        if (xamlStream is null)
        {
            return false;
        }

        result = XamlReader.Load(xamlStream);

        return true;
    }
}