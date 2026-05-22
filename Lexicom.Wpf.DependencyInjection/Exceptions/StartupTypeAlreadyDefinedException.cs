namespace Lexicom.Wpf.DependencyInjection.Exceptions;

public class StartupTypeAlreadyDefinedException(Type? currentStartupType) : Exception($"The '{nameof(WpfApplication)}' already has the defined startup type '{currentStartupType?.Name ?? "null"}'.")
{
}
