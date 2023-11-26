namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiPriorityAttribute(int priority = TuiPriorityAttribute.DEFAULT_PRIORITY) : Attribute
{
    public const int DEFAULT_PRIORITY = int.MaxValue;

    public int Priority { get; } = priority;
}