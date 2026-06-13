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

        string originalFilePathName = filePathName;

        string directoryPath = Path.GetDirectoryName(filePathName) ?? string.Empty;
        string fileName = Path.GetFileNameWithoutExtension(filePathName);
        string extension = Path.GetExtension(filePathName);

        int count = 1;
        while (File.Exists(filePathName))
        {
            if (count > OVERFLOW_MAX)
            {
                throw new UniqueFileNamePathOverflowException(originalFilePathName, OVERFLOW_MAX);
            }

            filePathName = Path.Combine(directoryPath, $"{fileName}({count}){extension}");

            count++;
        }

        return filePathName;
    }
}
