namespace Lexicom.Wpf.DependencyInjection.Exceptions;
public class StartupTypeAlreadyDefinedException : Exception
{
    public StartupTypeAlreadyDefinedException(Type? currentStartupType) : base($"The {nameof(WpfApplication)} already has a startup type defined '{currentStartupType?.Name ?? "null"}'")
    {
    }
}
