using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Exceptions;
public class CommandNotValidException(string? commandName, string? from, object? view, object? viewModel) : Exception($"The view '{view?.GetType().FullName ?? "null"}' with the view model '{viewModel?.GetType().FullName ?? "null"}' had a property called '{commandName ?? "null"}' which was automatically bound to the '{from ?? "null"}' of the view but the command was not of the type '{typeof(ICommand).FullName ?? "null"}'. Either rename your command to something other than '{commandName ?? "null"}' or change the type to '{typeof(ICommand).FullName ?? "null"}'")
{
}
