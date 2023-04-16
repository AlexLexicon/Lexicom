namespace Lexicom.Extensions.DateTimes;
public class DateTimeRelativeStringSettings
{
    public DateTimeRelativeStringSettings()
    {
        Now = DateTimeOffset.Now;
        NowString = null;
        MinimumDuration = DateTimeRelativeStringDuration.Seconds;
        MaximumDuration = DateTimeRelativeStringDuration.Years;
    }

    public DateTimeOffset Now { get; set; }
    public string? NowString { get; set; }
    public DateTimeRelativeStringDuration MinimumDuration { get; set; }
    public DateTimeRelativeStringDuration MaximumDuration { get; set; }
}
