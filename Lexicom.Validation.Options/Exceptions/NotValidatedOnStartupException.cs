namespace Lexicom.Validation.Options.Exceptions;
public class NotValidatedOnStartupException<T> : Exception
{
    public NotValidatedOnStartupException() : base($"The options '{typeof(T).Name}' was not valid but was configured to use a {nameof(AbstractOptionsValidator<T>)} at the application startup.")
    {
    }
}
