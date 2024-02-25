using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Exceptions;
public class LoadedCommandNotValidException(object? view, object? viewModel) : Exception($"The view '{view?.GetType().FullName}' with the view model '{viewModel?.GetType().FullName ?? "null"}' had a property called '{Constants.COMMAND_LOADED}' which was automatically bound to the 'OnInitializedAsync' of the view but the command was not of the type '{typeof(ICommand).FullName}'. Either rename your command to something other than '{Constants.COMMAND_LOADED}' or change the type to '{typeof(ICommand).FullName}'")
{
}
