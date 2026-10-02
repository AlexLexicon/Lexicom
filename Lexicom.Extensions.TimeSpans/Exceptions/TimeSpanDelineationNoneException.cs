namespace Lexicom.Extensions.TimeSpans.Exceptions;

public class TimeSpanDelineationNoneException() : Exception($"The delineation '{nameof(TimeSpanDelineation)}.{nameof(TimeSpanDelineation.None)}' is not valid for this function.")
{
}
