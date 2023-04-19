using Microsoft.AspNetCore.Components;
using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
public static class CommandExtensions
{
    public static EventCallback Bind(this ICommand command, object? parameter = null)
    {
        return new EventCallback(null, () =>
        {
            command.Execute(parameter);
        });
    }
}