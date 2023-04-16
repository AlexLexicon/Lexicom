namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiPageAttribute : Attribute
{
    public TuiPageAttribute(string? pagePath = null)
    {
        PagePath = pagePath;
    }

    public string? PagePath { get; }
}
