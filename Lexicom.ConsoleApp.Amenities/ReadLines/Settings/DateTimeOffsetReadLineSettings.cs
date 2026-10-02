namespace Lexicom.ConsoleApp.Amenities.ReadLines.Settings;

public class DateTimeOffsetReadLineSettings : ReadLineSettings
{
    public const string DEFAULT_FORMAT = "yyyy-MM-dd HH:mm:ss";

    public DateTimeOffsetReadLineSettings() : base()
    {
        Format = DEFAULT_FORMAT;
    }

    public string? Format { get; set; }
}
