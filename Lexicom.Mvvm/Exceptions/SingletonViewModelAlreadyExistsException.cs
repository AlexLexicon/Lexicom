namespace Lexicom.Mvvm.Exceptions;
public class SingletonViewModelAlreadyExistsException(Type? viewModelType) : Exception($"The singleton view model '{viewModelType?.FullName ?? "null"}' already exists so a new view model cannot be created.")
{
}
