namespace Lexicom.DependencyInjection.Hosting.Exceptions;
public class BeforeServiceProviderBuildServiceActivationNullException(Type? beforeServiceProviderBuildServiceType) : Exception($"An activated instance of the type '{beforeServiceProviderBuildServiceType?.FullName ?? "null"}' was null.")
{
}
