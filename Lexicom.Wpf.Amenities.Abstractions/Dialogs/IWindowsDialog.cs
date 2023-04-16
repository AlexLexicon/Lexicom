namespace Lexicom.Wpf.Amenities.Dialogs;
public interface IWindowsDialog
{
    string? SaveFile();
    /// <exception cref="ArgumentNullException"/>
    string? SaveFile(string filePathAndName);
    /// <exception cref="ArgumentNullException"/>
    string? SaveFile(SaveFileSettings settings);

    string? OpenFile();
    /// <exception cref="ArgumentNullException"/>
    string? OpenFile(string initalDirectoryPath);
    /// <exception cref="ArgumentNullException"/>
    string? OpenFile(OpenFileSettings settings);

    string? SelectDirectory();
    /// <exception cref="ArgumentNullException"/>
    string? SelectDirectory(string initalDirectoryPath);
    /// <exception cref="ArgumentNullException"/>
    string? SelectDirectory(SelectDirectorySettings settings);
}