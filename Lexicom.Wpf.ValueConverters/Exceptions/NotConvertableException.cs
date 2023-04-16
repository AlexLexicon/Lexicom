namespace Lexicom.Wpf.ValueConverters.Exceptions;
public class NotConvertableException : Exception
{
    public NotConvertableException() : base($"Cannot convert to specific value in value converter")
    {
    }
}
