namespace Lexicom.ConsoleApp.Tui;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TuiPageAttribute(string? pagePath = null) : Attribute
{
    public string? PagePath { get; } = pagePath;
}
