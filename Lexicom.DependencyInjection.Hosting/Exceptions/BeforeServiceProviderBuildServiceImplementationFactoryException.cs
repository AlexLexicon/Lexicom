namespace Lexicom.DependencyInjection.Hosting.Exceptions;
public class BeforeServiceProviderBuildServiceImplementationFactoryException() : Exception($"A '{nameof(IBeforeServiceProviderBuildService)}' was registered with a factory pattern implementation however since this is a pre build service a provider cannot be provided so factory patterns are not allowed.")
{
}
