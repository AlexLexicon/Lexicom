namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiTitleAttribute(string? title = null) : Attribute
{
    public string? Title { get; } = title;
}