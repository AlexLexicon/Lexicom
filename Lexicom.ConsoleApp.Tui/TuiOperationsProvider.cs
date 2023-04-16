namespace Lexicom.ConsoleApp.Tui;
public interface IAtlasOperationsProvider
{
    IReadOnlyList<Type> OperationTypes { get; }
}
public class TuiOperationsProvider : IAtlasOperationsProvider
{
    /// <exception cref="ArgumentNullException"/>
    public TuiOperationsProvider(IEnumerable<Type> operationTypes)
    {
        ArgumentNullException.ThrowIfNull(operationTypes);

        OperationTypes = operationTypes.ToList();
    }

    public IReadOnlyList<Type> OperationTypes { get; }
}
