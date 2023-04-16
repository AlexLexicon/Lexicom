namespace Lexicom.Smtp.Exceptions;
public class OutputDirectoryNotValidException : Exception
{
    public OutputDirectoryNotValidException(string? outputDirectoryPath, Exception? innerException) : base($"The output directory path '{outputDirectoryPath ?? "null"}' is not valid or does not exist and could not be created.", innerException)
    {
    }
}
