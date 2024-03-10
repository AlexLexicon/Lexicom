namespace Lexicom.DependencyInjection.Hosting.Exceptions;
public class PreBuildExecutorImplementationFactoryException() : Exception($"A '{nameof(IDependencyInjectionHostPreBuildService)}' was registered with a factory pattern implementation however since this is a pre build executor a service provider cannot be provided so factory patterns are not allowed.")
{
}
