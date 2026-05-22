namespace Lexicom.ConsoleApp.Tui;

public interface ITuiOperationsProvider
{
    IReadOnlyList<Type> OperationTypes { get; }
}
public class TuiOperationsProvider : ITuiOperationsProvider
{
    /// <exception cref="ArgumentNullException"/>
    public TuiOperationsProvider(IEnumerable<Type> operationTypes)
    {
        ArgumentNullException.ThrowIfNull(operationTypes);

        OperationTypes = operationTypes.ToList();
    }

    public IReadOnlyList<Type> OperationTypes { get; }
}
