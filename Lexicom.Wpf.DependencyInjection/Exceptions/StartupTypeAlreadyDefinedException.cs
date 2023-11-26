namespace Lexicom.Wpf.DependencyInjection.Exceptions;
public class StartupTypeAlreadyDefinedException(Type? currentStartupType) : Exception($"The {nameof(WpfApplication)} already has a startup type defined '{currentStartupType?.Name ?? "null"}'")
{
}
