namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiPriorityAttribute : Attribute
{
    public TuiPriorityAttribute(int? priority = null)
    {
        Priority = priority;
    }

    public int? Priority { get; }
}