namespace Lexicom.Mvvm.Exceptions;
public class ViewModelNotRegisteredException : Exception
{
    public ViewModelNotRegisteredException(Type? viewModelImplementationType, Exception? innerException) : base($"The view model for the implementation '{viewModelImplementationType?.FullName ?? "null"}' has not been registered successfully.", innerException)
    {
    }
}
