namespace Lexicom.Extensions.TimeSpans.Exceptions;

public class NoConfigurationsException() : Exception($"The provided '{nameof(HowLongAgoTextConfigurations)}' had no configurations.")
{
}
