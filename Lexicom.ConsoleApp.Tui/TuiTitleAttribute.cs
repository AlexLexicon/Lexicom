namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiTitleAttribute : Attribute
{
    public TuiTitleAttribute(string? title = null)
    {
        Title = title;
    }

    public string? Title { get; }
}