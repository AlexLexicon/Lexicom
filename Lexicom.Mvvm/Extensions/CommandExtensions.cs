using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace Lexicom.Mvvm.Extensions;

public static class CommandExtensions
{
    /// <summary>
    /// Calls the provided <see cref="ICommand"/> value's <see cref="ICommand.Execute(object?)"/> method, unless the provided <see cref="ICommand"/> value is of the type <see cref="IAsyncRelayCommand"/> in which case it will call the <see cref="IAsyncRelayCommand.ExecuteAsync(object?)"/> method instead.
    /// </summary>
    public static async Task ExecuteSyncOrAsync(this ICommand command, object? parameter = null)
    {
        if (command is IAsyncRelayCommand asyncRelayCommand)
        {
            await asyncRelayCommand.ExecuteAsync(parameter);
        }
        else
        {
            command.Execute(parameter);
        }
    }
}
