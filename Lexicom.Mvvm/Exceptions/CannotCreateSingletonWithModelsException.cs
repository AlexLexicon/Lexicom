namespace Lexicom.Mvvm.Exceptions;
public class CannotCreateSingletonWithModelsException(Type? viewModelType) : Exception($"Cannot create a new singleton view model '{viewModelType?.GetType().FullName ?? "null"}' with diffrent models since a singleton instance already exists.")
{
}
