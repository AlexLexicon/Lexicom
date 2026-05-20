namespace Lexicom.Extensions.TimeSpans.UnitTests.Tests.TimeSpanExtensionsTests;

public class ToHowLongAgoTextTests
{
    public static TheoryData<TimeSpan, string> DaysHoursMinutes_Configurations_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0), "less than a minute ago"),
        (new TimeSpan(0, 23, 0, 0), "23 hours ago"),
        (new TimeSpan(1, 0, 0, 0), "a day ago"),
        (new TimeSpan(1, 23, 0, 0), "a day ago"),
        (new TimeSpan(2, 0, 0, 0), "2 days ago"),
        (new TimeSpan(2, 23, 0, 0), "2 days ago"),
        (new TimeSpan(5, 0, 0, 0), "5 days ago"),
        (new TimeSpan(999, 0, 0, 0), "999 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Days, 0, 0, 0), $"{TimeSpan.MaxValue.Days} days ago"),
        (new TimeSpan(0, 0, 0), "less than a minute ago"),
        (new TimeSpan(0, 59, 0), "59 minutes ago"),
        (new TimeSpan(1, 0, 0), "an hour ago"),
        (new TimeSpan(1, 59, 0), "an hour ago"),
        (new TimeSpan(2, 0, 0), "2 hours ago"),
        (new TimeSpan(2, 59, 0), "2 hours ago"),
        (new TimeSpan(5, 0, 0), "5 hours ago"),
        (new TimeSpan(999, 0, 0), "41 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Hours, 0, 0), $"{TimeSpan.MaxValue.Hours} hours ago"),
        (new TimeSpan(0, 0, 59), "less than a minute ago"),
        (new TimeSpan(0, 1, 0), "a minute ago"),
        (new TimeSpan(0, 1, 59), "a minute ago"),
        (new TimeSpan(0, 2, 0), "2 minutes ago"),
        (new TimeSpan(0, 2, 59), "2 minutes ago"),
        (new TimeSpan(0, 5, 0), "5 minutes ago"),
        (new TimeSpan(0, 999, 0), "16 hours ago"),
        (new TimeSpan(0, TimeSpan.MaxValue.Minutes, 0), $"{TimeSpan.MaxValue.Minutes} minutes ago"),
        (new TimeSpan(0, 0, 0, 0, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 1, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 1, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 2, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 2, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 5, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 999, 0), "16 minutes ago"),
        (new TimeSpan(0, 0, 0, TimeSpan.MaxValue.Seconds, 0), $"less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 999), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 5, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, 999, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 0, 0, TimeSpan.MaxValue.Milliseconds, 0), $"less than a minute ago"),
        (TimeSpan.FromTicks(0), "less than a minute ago"),
        (TimeSpan.FromTicks(1), "less than a minute ago"),
        (TimeSpan.FromTicks(5), "less than a minute ago"),
        (TimeSpan.FromTicks(999), "less than a minute ago"),
        (TimeSpan.FromTicks(TimeSpan.MaxValue.Nanoseconds), $"less than a minute ago"),
    ];
    [Theory]
    [MemberData(nameof(DaysHoursMinutes_Configurations_Text_Is_Correct_Data))]
    public void DaysHoursMinutes_Configurations_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(HowLongAgoTextConfigurations.DaysHoursMinutes);

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Standard_Configurations_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0), "a second ago"),
        (new TimeSpan(0, 23, 0, 0), "23 hours ago"),
        (new TimeSpan(1, 0, 0, 0), "a day ago"),
        (new TimeSpan(1, 23, 0, 0), "a day ago"),
        (new TimeSpan(2, 0, 0, 0), "2 days ago"),
        (new TimeSpan(2, 23, 0, 0), "2 days ago"),
        (new TimeSpan(5, 0, 0, 0), "5 days ago"),
        (new TimeSpan(999, 0, 0, 0), "999 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Days, 0, 0, 0), $"{TimeSpan.MaxValue.Days} days ago"),
        (new TimeSpan(0, 59, 0), "59 minutes ago"),
        (new TimeSpan(1, 0, 0), "an hour ago"),
        (new TimeSpan(1, 59, 0), "an hour ago"),
        (new TimeSpan(2, 0, 0), "2 hours ago"),
        (new TimeSpan(2, 59, 0), "2 hours ago"),
        (new TimeSpan(5, 0, 0), "5 hours ago"),
        (new TimeSpan(999, 0, 0), "41 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Hours, 0, 0), $"{TimeSpan.MaxValue.Hours} hours ago"),
        (new TimeSpan(0, 0, 0), "a second ago"),
        (new TimeSpan(0, 0, 59), "59 seconds ago"),
        (new TimeSpan(0, 1, 0), "a minute ago"),
        (new TimeSpan(0, 1, 59), "a minute ago"),
        (new TimeSpan(0, 2, 0), "2 minutes ago"),
        (new TimeSpan(0, 2, 59), "2 minutes ago"),
        (new TimeSpan(0, 5, 0), "5 minutes ago"),
        (new TimeSpan(0, 999, 0), "16 hours ago"),
        (new TimeSpan(0, TimeSpan.MaxValue.Minutes, 0), $"{TimeSpan.MaxValue.Minutes} minutes ago"),
        (new TimeSpan(0, 0, 0, 0, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 1, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 1, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 2, 0), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 2, 999), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 5, 0), "5 seconds ago"),
        (new TimeSpan(0, 0, 0, 999, 0), "16 minutes ago"),
        (new TimeSpan(0, 0, 0, TimeSpan.MaxValue.Seconds, 0), $"{TimeSpan.MaxValue.Seconds} seconds ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 5, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 999, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, TimeSpan.MaxValue.Milliseconds, 0), $"a second ago"),
        (TimeSpan.FromTicks(0), "a second ago"),
        (TimeSpan.FromTicks(1), "a second ago"),
        (TimeSpan.FromTicks(5), "a second ago"),
        (TimeSpan.FromTicks(999), "a second ago"),
        (TimeSpan.FromTicks(TimeSpan.MaxValue.Nanoseconds), $"a second ago"),
    ];
    [Theory]
    [MemberData(nameof(Standard_Configurations_Text_Is_Correct_Data))]
    public void Standard_Configurations_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(HowLongAgoTextConfigurations.Standard);

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> All_Configurations_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0), "a nanosecond ago"),
        (new TimeSpan(0, 23, 0, 0), "23 hours ago"),
        (new TimeSpan(1, 0, 0, 0), "a day ago"),
        (new TimeSpan(1, 23, 0, 0), "a day ago"),
        (new TimeSpan(2, 0, 0, 0), "2 days ago"),
        (new TimeSpan(2, 23, 0, 0), "2 days ago"),
        (new TimeSpan(5, 0, 0, 0), "5 days ago"),
        (new TimeSpan(999, 0, 0, 0), "999 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Days, 0, 0, 0), $"{TimeSpan.MaxValue.Days} days ago"),
        (new TimeSpan(0, 0, 0), "a nanosecond ago"),
        (new TimeSpan(0, 59, 0), "59 minutes ago"),
        (new TimeSpan(1, 0, 0), "an hour ago"),
        (new TimeSpan(1, 59, 0), "an hour ago"),
        (new TimeSpan(2, 0, 0), "2 hours ago"),
        (new TimeSpan(2, 59, 0), "2 hours ago"),
        (new TimeSpan(5, 0, 0), "5 hours ago"),
        (new TimeSpan(999, 0, 0), "41 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Hours, 0, 0), $"{TimeSpan.MaxValue.Hours} hours ago"),
        (new TimeSpan(0, 0, 59), "59 seconds ago"),
        (new TimeSpan(0, 1, 0), "a minute ago"),
        (new TimeSpan(0, 1, 59), "a minute ago"),
        (new TimeSpan(0, 2, 0), "2 minutes ago"),
        (new TimeSpan(0, 2, 59), "2 minutes ago"),
        (new TimeSpan(0, 5, 0), "5 minutes ago"),
        (new TimeSpan(0, 999, 0), "16 hours ago"),
        (new TimeSpan(0, TimeSpan.MaxValue.Minutes, 0), $"{TimeSpan.MaxValue.Minutes} minutes ago"),
        (new TimeSpan(0, 0, 0, 0, 0), "a nanosecond ago"),
        (new TimeSpan(0, 0, 0, 0, 999), "999 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 1, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 1, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 2, 0), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 2, 999), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 5, 0), "5 seconds ago"),
        (new TimeSpan(0, 0, 0, 999, 0), "16 minutes ago"),
        (new TimeSpan(0, 0, 0, TimeSpan.MaxValue.Seconds, 0), $"{TimeSpan.MaxValue.Seconds} seconds ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 0), "a nanosecond ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 999), "999 microseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 999), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), "2 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 999), "2 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 5, 0), "5 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 999, 0), "999 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, TimeSpan.MaxValue.Milliseconds, 0), $"{TimeSpan.MaxValue.Milliseconds} milliseconds ago"),
        (TimeSpan.FromTicks(0), "a nanosecond ago"),
        (TimeSpan.FromTicks(1), "100 nanoseconds ago"),
        (TimeSpan.FromTicks(5), "500 nanoseconds ago"),
        (TimeSpan.FromTicks(999), "99 microseconds ago"),
        (TimeSpan.FromTicks(TimeSpan.MaxValue.Nanoseconds), $"70 microseconds ago"),
    ];
    [Theory]
    [MemberData(nameof(All_Configurations_Text_Is_Correct_Data))]
    public void All_Configurations_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(HowLongAgoTextConfigurations.All);

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Days_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0), "less than a day ago"),
        (new TimeSpan(0, 23, 0, 0), "less than a day ago"),
        (new TimeSpan(1, 0, 0, 0), "a day ago"),
        (new TimeSpan(1, 23, 0, 0), "a day ago"),
        (new TimeSpan(2, 0, 0, 0), "2 days ago"),
        (new TimeSpan(2, 23, 0, 0), "2 days ago"),
        (new TimeSpan(5, 0, 0, 0), "5 days ago"),
        (new TimeSpan(999, 0, 0, 0), "999 days ago"),
        (new TimeSpan(TimeSpan.MaxValue.Days, 0, 0, 0), $"{TimeSpan.MaxValue.Days} days ago"),
    ];
    [Theory]
    [MemberData(nameof(Days_Text_Is_Correct_Data))]
    public void Days_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Days),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Hours_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0), "less than an hour ago"),
        (new TimeSpan(0, 59, 0), "less than an hour ago"),
        (new TimeSpan(1, 0, 0), "an hour ago"),
        (new TimeSpan(1, 59, 0), "an hour ago"),
        (new TimeSpan(2, 0, 0), "2 hours ago"),
        (new TimeSpan(2, 59, 0), "2 hours ago"),
        (new TimeSpan(5, 0, 0), "5 hours ago"),
        (new TimeSpan(999, 0, 0), "999 hours ago"),
        (new TimeSpan(TimeSpan.MaxValue.Hours, 0, 0), $"{TimeSpan.MaxValue.Hours} hours ago"),
    ];
    [Theory]
    [MemberData(nameof(Hours_Text_Is_Correct_Data))]
    public void Hours_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Hours),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Minutes_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0), "less than a minute ago"),
        (new TimeSpan(0, 0, 59), "less than a minute ago"),
        (new TimeSpan(0, 1, 0), "a minute ago"),
        (new TimeSpan(0, 1, 59), "a minute ago"),
        (new TimeSpan(0, 2, 0), "2 minutes ago"),
        (new TimeSpan(0, 2, 59), "2 minutes ago"),
        (new TimeSpan(0, 5, 0), "5 minutes ago"),
        (new TimeSpan(0, 999, 0), "999 minutes ago"),
        (new TimeSpan(0, TimeSpan.MaxValue.Minutes, 0), $"{TimeSpan.MaxValue.Minutes} minutes ago"),
    ];
    [Theory]
    [MemberData(nameof(Minutes_Text_Is_Correct_Data))]
    public void Minutes_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Minutes),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Seconds_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 0, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 1, 0), "a second ago"),
        (new TimeSpan(0, 0, 0, 1, 999), "a second ago"),
        (new TimeSpan(0, 0, 0, 2, 0), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 2, 999), "2 seconds ago"),
        (new TimeSpan(0, 0, 0, 5, 0), "5 seconds ago"),
        (new TimeSpan(0, 0, 0, 999, 0), "999 seconds ago"),
        (new TimeSpan(0, 0, 0, TimeSpan.MaxValue.Seconds, 0), $"{TimeSpan.MaxValue.Seconds} seconds ago"),
    ];
    [Theory]
    [MemberData(nameof(Seconds_Text_Is_Correct_Data))]
    public void Seconds_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Seconds),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Milliseconds_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0, 0, 0), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 999), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 1, 999), "a millisecond ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), "2 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 2, 999), "2 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 5, 0), "5 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 999, 0), "999 milliseconds ago"),
        (new TimeSpan(0, 0, 0, 0, TimeSpan.MaxValue.Milliseconds, 0), $"{TimeSpan.MaxValue.Milliseconds} milliseconds ago"),
    ];
    [Theory]
    [MemberData(nameof(Milliseconds_Text_Is_Correct_Data))]
    public void Milliseconds_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Milliseconds),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Microseconds_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0, 0, 0), "a microsecond ago"),
        (TimeSpan.FromTicks(9), "a microsecond ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 1), "a microsecond ago"),
        (TimeSpan.FromTicks(19), "a microsecond ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 2), "2 microseconds ago"),
        (TimeSpan.FromTicks(29), "2 microseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 5), "5 microseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 0, 999), "999 microseconds ago"),
        (new TimeSpan(0, 0, 0, 0, 0, TimeSpan.MaxValue.Microseconds), $"{TimeSpan.MaxValue.Microseconds} microseconds ago"),
    ];
    [Theory]
    [MemberData(nameof(Microseconds_Text_Is_Correct_Data))]
    public void Microseconds_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Microseconds),
        });

        //assert
        Assert.Equal(expectedText, text);
    }

    public static TheoryData<TimeSpan, string> Nanoseconds_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (TimeSpan.FromTicks(0), "a nanosecond ago"),
        (TimeSpan.FromTicks(1), "100 nanoseconds ago"),
        (TimeSpan.FromTicks(5), "500 nanoseconds ago"),
        (TimeSpan.FromTicks(999), "99900 nanoseconds ago"),
        (TimeSpan.FromTicks(TimeSpan.MaxValue.Nanoseconds), $"{TimeSpan.MaxValue.Nanoseconds}00 nanoseconds ago"),
    ];
    [Theory]
    [MemberData(nameof(Nanoseconds_Text_Is_Correct_Data))]
    public void Nanoseconds_Text_Is_Correct(TimeSpan timeSpan, string expectedText)
    {
        //act
        string text = timeSpan.ToHowLongAgoText(new HowLongAgoTextConfigurations
        {
            new HowLongAgoTextConfiguration(TimeSpanDelineation.Nanoseconds),
        });

        //assert
        Assert.Equal(expectedText, text);
    }
}
