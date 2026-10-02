using Lexicom.Mvvm.Extensions;

namespace Lexicom.Mvvm.Exceptions;

public class ViewModelNotRegisteredException(Type? viewModelImplementationType, Exception? innerException) : Exception($"The view model for the implementation '{viewModelImplementationType?.FullName ?? "null"}' has not been registered successfully. Make sure you are calling '{nameof(IMvvmServiceBuilder)}.{nameof(MvvmServiceBuilderExtensions.AddViewModel)}()' during your service registration.", innerException)
{
}
