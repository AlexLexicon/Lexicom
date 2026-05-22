namespace Lexicom.ConsoleApp.Amenities.ReadLines.Settings;
public class ReadLineSettings
{
    public ReadLineSettings()
    {
        var copy = Consolex.CopyDefaultReadLineSettings();

        CancelKey = copy.CancelKey;
        DefaultKey = copy.DefaultKey;
        DefaultInput = copy.DefaultInput;
        InitialInput = copy.InitialInput;
        InputColor = copy.InputColor;
    }

    internal ReadLineSettings(
        ConsoleKey? cancelKey, 
        ConsoleKey? defaultKey, 
        string? defaultInput, 
        string? initialInput,
        ConsoleColor? inputColor)
    {
        CancelKey = cancelKey;
        DefaultKey = defaultKey;
        DefaultInput = defaultInput;
        InitialInput = initialInput;
        InputColor = inputColor;
    }

    public ConsoleKey? CancelKey { get; set; }
    public ConsoleKey? DefaultKey { get; set; }
    public string? DefaultInput { get; set; }
    public string? InitialInput { get; set; }
    public ConsoleColor? InputColor { get; set; }
}
