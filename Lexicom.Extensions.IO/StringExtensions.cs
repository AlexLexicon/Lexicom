using Lexicom.Extensions.IO.Exceptions;

namespace Lexicom.Extensions.IO;
public static class StringExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="UniqueFileNamePathOverflowException"/>
    public static string GetUniqueFileNamePath(this string filePathName)
    {
        ArgumentNullException.ThrowIfNull(filePathName);

        const int OVERFLOW_MAX = 10000;

        int count = 1;
        string originalFilePathName = filePathName;

        string directoryPath = Path.GetDirectoryName(filePathName) ?? string.Empty;
        string fileName = Path.GetFileNameWithoutExtension(filePathName) + "{0}";
        string extension = Path.GetExtension(filePathName);

        int overflow = 0;
        while (File.Exists(filePathName) && overflow < OVERFLOW_MAX)
        {
            string newFilePathName = string.Format(fileName, $"({count++}){extension}");

            filePathName = Path.Combine(directoryPath, newFilePathName);
        }

        if (overflow >= OVERFLOW_MAX)
        {
            throw new UniqueFileNamePathOverflowException(originalFilePathName, overflow);
        }

        return filePathName;
    }
}
