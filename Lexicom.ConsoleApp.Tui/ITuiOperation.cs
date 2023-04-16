namespace Lexicom.ConsoleApp.Tui;
public interface ITuiOperation
{
    //return true if the exception is handled and the operation should continue
    public Task<bool> HandleExceptionAsync(Exception exception)
    {
        //by default dont handle the exception
        return Task.FromResult(false);
    }

    public Task ExecuteAsync();
}
