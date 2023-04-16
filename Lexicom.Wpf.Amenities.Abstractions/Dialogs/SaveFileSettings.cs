namespace Lexicom.Wpf.Amenities.Dialogs;
public class SaveFileSettings
{
    public string? Title { get; set; } = "Save File";
    public string? FileName { get; set; }
    public string? InitialDirectory { get; set; }
    public string? DefaultExtension { get; set; }
    public bool? EnsureValidNames { get; set; } = true;
}
