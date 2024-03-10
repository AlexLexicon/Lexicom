namespace Lexicom.DependencyInjection.Hosting.Exceptions;
public class PreBuildExecutorMissingParameterlessConstructorException(Type? implementationType, Exception? innerException) : Exception($"The implementation insance of '{implementationType?.FullName ?? "null"}' for a '{nameof(IBeforeServiceProviderBuildService)}' interface does not implement a default parameterless constructor which is required for a pre build executor implementation.", innerException)
{
}
