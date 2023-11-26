namespace Lexicom.Smtp.Exceptions;
public class OutputDirectoryNotValidException(string? outputDirectoryPath, Exception? innerException) 
    : Exception($"The output directory path '{outputDirectoryPath ?? "null"}' is not valid or does not exist and could not be created.", innerException)
{
}
