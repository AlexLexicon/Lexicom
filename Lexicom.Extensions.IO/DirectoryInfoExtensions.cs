namespace Lexicom.Extensions.IO;
public static class DirectoryInfoExtensions
{
    /// <exception cref="ArgumentNullException"/>
    public static long RecursiveSizeInBytes(this DirectoryInfo directoryInfo)
    {
        ArgumentNullException.ThrowIfNull(directoryInfo);

        long size = 0;

        FileInfo[] files = directoryInfo.GetFiles();
        foreach (FileInfo file in files)
        {
            size += file.Length;
        }

        DirectoryInfo[] directories = directoryInfo.GetDirectories();
        foreach (DirectoryInfo directory in directories)
        {
            size += RecursiveSizeInBytes(directory);
        }

        return size;
    }
}
