namespace Lexicom.DependencyInjection.Hosting.Exceptions;
public class BeforeServiceProviderBuildServiceCastException(Type? beforeServiceProviderBuildServiceType, Exception? innerException) : Exception($"Failed to cast the type '{beforeServiceProviderBuildServiceType?.FullName ?? "null"}' to the type '{nameof(IBeforeServiceProviderBuildService)}'.", innerException)
{
}
