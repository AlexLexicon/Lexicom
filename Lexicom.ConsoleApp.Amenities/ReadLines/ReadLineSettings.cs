namespace Lexicom.ConsoleApp.Amenities;
public class ReadLineSettings
{
    public ReadLineSettings()
    {
        var copy = Consolex.CopyDefaultReadLineSettings();

        CancelKey = copy.CancelKey;
        DefaultKey = copy.DefaultKey;
        DefaultInput = copy.DefaultInput;
        InitalInput = copy.InitalInput;
    }

    internal ReadLineSettings(
        ConsoleKey? cancelKey, 
        ConsoleKey? defaultKey, 
        string? defaultInput, 
        string? initalInput)
    {
        CancelKey = cancelKey;
        DefaultKey = defaultKey;
        DefaultInput = defaultInput;
        InitalInput = initalInput;
    }

    public ConsoleKey? CancelKey { get; set; }
    public ConsoleKey? DefaultKey { get; set; }
    public string? DefaultInput { get; set; }
    public string? InitalInput { get; set; }
}
