namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiPriorityAttribute : Attribute
{
    public const int DEFAULT_PRIORITY = int.MaxValue;

    public TuiPriorityAttribute(int priority = DEFAULT_PRIORITY)
    {
        Priority = priority;
    }

    public int Priority { get; }
}