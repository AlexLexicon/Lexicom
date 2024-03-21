using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
internal class CommandHandleEvent : IHandleEvent
{
    private readonly ICommand _command;
    private readonly object? _directArg;

    /// <exception cref="ArgumentNullException"/>
    public CommandHandleEvent(
        ICommand command, 
        object? directArg)
    {
        ArgumentNullException.ThrowIfNull(command);

        _command = command;
        _directArg = directArg;
    }

    public async Task HandleEventAsync(EventCallbackWorkItem item, object? arg)
    {
        object? actualArg = _directArg is not null ? _directArg : arg;

        if (_command is IAsyncRelayCommand asyncRelayCommand)
        {
            await asyncRelayCommand.ExecuteAsync(actualArg);
        }
        else
        {
            _command.Execute(actualArg);
        }
    }
}
