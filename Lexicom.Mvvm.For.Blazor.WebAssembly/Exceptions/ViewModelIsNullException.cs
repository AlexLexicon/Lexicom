namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Exceptions;
public class ViewModelIsNullException(object? view) : Exception($"The view '{view?.GetType().FullName}' had a null view model. Be sure to set the ViewModel property on 'Component' inherited views and make sure dependency injection is setup for all view models.")
{
}
