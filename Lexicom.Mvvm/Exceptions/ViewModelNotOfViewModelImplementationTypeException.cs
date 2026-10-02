namespace Lexicom.Mvvm.Exceptions;

public class ViewModelNotOfViewModelImplementationTypeException<TViewModelImplementation>(object? viewModel) : Exception($"The provided view model was of the type '{viewModel?.GetType()?.Name ?? "null"}' but must be of the type '{typeof(TViewModelImplementation).Name}'.")
{
}
