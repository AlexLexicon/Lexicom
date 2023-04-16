using System.Reflection;

namespace Lexicom.ConsoleApp.Tui;
internal class TuiOperationDefinition
{
    /// <exception cref="ArgumentNullException"/>
    public TuiOperationDefinition(Type operationType)
    {
        ArgumentNullException.ThrowIfNull(operationType);

        OperationType = operationType;

        PageAttribute = (TuiPageAttribute?)operationType.GetCustomAttribute(typeof(TuiPageAttribute));
        TitleAttribute = (TuiTitleAttribute?)operationType.GetCustomAttribute(typeof(TuiTitleAttribute));

        Title = TitleAttribute?.Title ?? operationType.Name;
    }

    public Type OperationType { get; }
    public TuiPageAttribute? PageAttribute { get; }
    public TuiTitleAttribute? TitleAttribute { get; }
    public string Title { get; }
}