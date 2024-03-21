using Microsoft.AspNetCore.Components;
using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly.Extensions;
public static class CommandExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static Action BindAfter(this ICommand command, object? parameter = null)
    {
        ArgumentNullException.ThrowIfNull(command);

        return () =>
        {
            command.Execute(parameter);
        };
    }

    /// <exception cref="ArgumentNullException"/>
    public static EventCallback Bind(this ICommand command, object? arg = null)
    {
        ArgumentNullException.ThrowIfNull(command);

        var commandHandleEvent = new CommandHandleEvent(command, arg);

        return new EventCallback(commandHandleEvent, @delegate: null);
    }
}