using Lexicom.Extensions.TimeSpans;
using Lexicom.Extensions.TimeSpans.Exceptions;

namespace UnitTests.For.Lexicom.Extensions.TimeSpans.TimeSpanExtensionsTests;

public class ToDurationTextTests
{
    public static TheoryData<TimeSpan, TimeSpanDelineation, string> Multiple_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "0 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Days 5 Hours 5 Minutes 5 Seconds 5 Milliseconds 5 Microseconds 500 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds, "5 Days 5 Hours 5 Minutes 5 Seconds 5 Milliseconds 5 Microseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds, "5 Days 5 Hours 5 Minutes 5 Seconds 5 Milliseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds, "5 Days 5 Hours 5 Minutes 5 Seconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes, "5 Days 5 Hours 5 Minutes"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Days | TimeSpanDelineation.Hours, "5 Days 5 Hours"),        
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Hours 5 Minutes 5 Seconds 5 Milliseconds 5 Microseconds 500 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Minutes 5 Seconds 5 Milliseconds 5 Microseconds 500 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Seconds 5 Milliseconds 5 Microseconds 500 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Milliseconds 5 Microseconds 500 Nanoseconds"),
        (new TimeSpan(5, 5, 5, 5, 5, 5).Add(TimeSpan.FromTicks(5)), TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "5 Microseconds 500 Nanoseconds"),
    ];
    [Theory]
    [MemberData(nameof(Multiple_Text_Is_Correct_Data))]
    public void Multiple_Text_Is_Correct(TimeSpan timeSpan, TimeSpanDelineation inlcude, string epxectedText)
    {
        //act
        string text = timeSpan.ToDurationText(inlcude);

        //assert
        Assert.Equal(epxectedText, text);
    }

    public static TheoryData<TimeSpan, TimeSpanDelineation, string> Individual_Text_Is_Correct_Data { get; } =
    [
        //arrange
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Days, "0 Days"),
        (new TimeSpan(1, 0, 0, 0, 0, 0), TimeSpanDelineation.Days, "1 Day"),
        (new TimeSpan(2, 0, 0, 0, 0, 0), TimeSpanDelineation.Days, "2 Days"),
        (new TimeSpan(1, 0, 0, 0, 0, 0), TimeSpanDelineation.Days | TimeSpanDelineation.Hours, "1 Day"),
        (new TimeSpan(2, 0, 0, 0, 0, 0), TimeSpanDelineation.Days | TimeSpanDelineation.Hours, "2 Days"),
                   
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Hours, "0 Hours"),
        (new TimeSpan(0, 1, 0, 0, 0, 0), TimeSpanDelineation.Hours, "1 Hour"),
        (new TimeSpan(0, 2, 0, 0, 0, 0), TimeSpanDelineation.Hours, "2 Hours"),
        (new TimeSpan(0, 1, 0, 0, 0, 0), TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes, "1 Hour"),
        (new TimeSpan(0, 2, 0, 0, 0, 0), TimeSpanDelineation.Hours | TimeSpanDelineation.Minutes, "2 Hours"),
                         
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Minutes, "0 Minutes"),
        (new TimeSpan(0, 0, 1, 0, 0, 0), TimeSpanDelineation.Minutes, "1 Minute"),
        (new TimeSpan(0, 0, 2, 0, 0, 0), TimeSpanDelineation.Minutes, "2 Minutes"),
        (new TimeSpan(0, 0, 1, 0, 0, 0), TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds, "1 Minute"),
        (new TimeSpan(0, 0, 2, 0, 0, 0), TimeSpanDelineation.Minutes | TimeSpanDelineation.Seconds, "2 Minutes"),
                      
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Seconds, "0 Seconds"),
        (new TimeSpan(0, 0, 0, 1, 0, 0), TimeSpanDelineation.Seconds, "1 Second"),
        (new TimeSpan(0, 0, 0, 2, 0, 0), TimeSpanDelineation.Seconds, "2 Seconds"),
        (new TimeSpan(0, 0, 0, 1, 0, 0), TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds, "1 Second"),
        (new TimeSpan(0, 0, 0, 2, 0, 0), TimeSpanDelineation.Seconds | TimeSpanDelineation.Milliseconds, "2 Seconds"),
                           
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Milliseconds, "0 Milliseconds"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), TimeSpanDelineation.Milliseconds, "1 Millisecond"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), TimeSpanDelineation.Milliseconds, "2 Milliseconds"),
        (new TimeSpan(0, 0, 0, 0, 1, 0), TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds, "1 Millisecond"),
        (new TimeSpan(0, 0, 0, 0, 2, 0), TimeSpanDelineation.Milliseconds | TimeSpanDelineation.Microseconds, "2 Milliseconds"),
                      
        (new TimeSpan(0, 0, 0, 0, 0, 0), TimeSpanDelineation.Microseconds, "0 Microseconds"),
        (new TimeSpan(0, 0, 0, 0, 0, 1), TimeSpanDelineation.Microseconds, "1 Microsecond"),
        (new TimeSpan(0, 0, 0, 0, 0, 2), TimeSpanDelineation.Microseconds, "2 Microseconds"),
        (new TimeSpan(0, 0, 0, 0, 0, 1), TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "1 Microsecond"),
        (new TimeSpan(0, 0, 0, 0, 0, 2), TimeSpanDelineation.Microseconds | TimeSpanDelineation.Nanoseconds, "2 Microseconds"),

        (TimeSpan.FromTicks(1), TimeSpanDelineation.Nanoseconds, "100 Nanoseconds"),
        (TimeSpan.FromTicks(2), TimeSpanDelineation.Nanoseconds, "200 Nanoseconds"),
    ];
    [Theory]
    [MemberData(nameof(Individual_Text_Is_Correct_Data))]
    public void Individual_Text_Is_Correct(TimeSpan timeSpan, TimeSpanDelineation inlcude, string epxectedText)
    {
        //act
        string text = timeSpan.ToDurationText(inlcude);

        //assert
        Assert.Equal(epxectedText, text);
    }

    [Fact]
    public void Fail_When_Include_Is_Only_None()
    {
        //arrange
        var timeSpan = new TimeSpan(5, 5, 5, 5, 5, 5);

        //assert
        Assert.Throws<TimeSpanDelineationNoneException>(() =>
        {
            //act
            timeSpan.ToDurationText(TimeSpanDelineation.None);
        });
    }

    [Theory]
    [InlineData(TimeSpanDelineation.None | TimeSpanDelineation.Microseconds, "5 Microseconds")]
    [InlineData(TimeSpanDelineation.None | TimeSpanDelineation.Seconds, "5 Seconds")]
    [InlineData(TimeSpanDelineation.None | TimeSpanDelineation.Minutes, "5 Minutes")]
    [InlineData(TimeSpanDelineation.None | TimeSpanDelineation.Hours, "5 Hours")]
    [InlineData(TimeSpanDelineation.None | TimeSpanDelineation.Days, "5 Days")]
    public void Successful_When_Include_None(TimeSpanDelineation include, string expectedText)
    {
        //assert
        var timeSpan = new TimeSpan(5, 5, 5, 5, 5, 5);

        //act
        var text = timeSpan.ToDurationText(include);

        //assert
        Assert.Equal(expectedText, text);
    }
}
