namespace Lexicom.Wpf.Amenities.Dialogs;
public class OpenFileSettings
{
    public string? Title { get; set; } = "Open File";
    public string? FileName { get; set; }
    public string? InitialDirectory { get; set; }
    public string? DefaultExtension { get; set; }
    public bool? EnsureFileExists { get; set; } = true;
    public bool? EnsurePathExists { get; set; } = true;
}
