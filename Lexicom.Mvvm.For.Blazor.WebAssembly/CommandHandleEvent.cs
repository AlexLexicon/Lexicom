using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.Components;
using System.Windows.Input;

namespace Lexicom.Mvvm.For.Blazor.WebAssembly;
internal class CommandHandleEvent : IHandleEvent
{
    private readonly ICommand _command;

    /// <exception cref="ArgumentNullException"/>
    public CommandHandleEvent(ICommand command)
    {
        ArgumentNullException.ThrowIfNull(command);

        _command = command;
    }

    public async Task HandleEventAsync(EventCallbackWorkItem item, object? arg)
    {
        if (_command is IAsyncRelayCommand asyncRelayCommand)
        {
            await asyncRelayCommand.ExecuteAsync(arg);
        }
        else
        {
            _command.Execute(arg);
        }
    }
}
