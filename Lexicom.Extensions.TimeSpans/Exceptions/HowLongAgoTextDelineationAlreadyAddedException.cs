namespace Lexicom.Extensions.TimeSpans.Exceptions;

public class HowLongAgoTextDelineationAlreadyAddedException(TimeSpanDelineation delineation) : Exception($"The time span delineation '{delineation}' has already been added.")
{
}
