namespace Lexicom.Extensions.TimeSpans;
public class TimeSpanShortestStringSettings
{
    public TimeSpanShortestStringSettings()
    {
        NowString = null;
        ShortestDurationDescription = TimeSpanShortestStringDurationDescription.Seconds;
        LongestDurationDescription = TimeSpanShortestStringDurationDescription.Days;
    }

    public string? NowString { get; set; }
    public TimeSpanShortestStringDurationDescription ShortestDurationDescription { get; set; }
    public TimeSpanShortestStringDurationDescription LongestDurationDescription { get; set; }
}
