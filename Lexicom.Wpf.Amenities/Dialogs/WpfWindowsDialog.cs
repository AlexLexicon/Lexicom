using Lexicom.Wpf.Amenities.Extensions;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicom.Wpf.Amenities.Dialogs;
public class WpfWindowsDialog : IWindowsDialog
{
    /// <exception cref="ArgumentNullException"/>
    public static string? InvokeShowDialogOnCommonSaveFileDialog(Action<CommonSaveFileDialog> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var commonSaveFileDialog = new CommonSaveFileDialog();

        configure.Invoke(commonSaveFileDialog);

        return commonSaveFileDialog.GetFilePathFromShowDialog();
    }
    /// <exception cref="ArgumentNullException"/>
    public static string? InvokeShowDialogOnCommonOpenFileDialog(Action<CommonOpenFileDialog> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);

        var commonOpenFileDialog = new CommonOpenFileDialog();

        configure.Invoke(commonOpenFileDialog);

        return commonOpenFileDialog.GetFilePathFromShowDialog();
    }

    public string? SaveFile() => SaveFile(new SaveFileSettings());
    /// <exception cref="ArgumentNullException"/>
    public string? SaveFile(string filePathAndName)
    {
        ArgumentNullException.ThrowIfNull(filePathAndName);

        var settings = new SaveFileSettings();

        //the following tries to seperate the file path and name
        //into the fileName, directory and extension variables
        string[] pathSegments = filePathAndName.Split('\\');

        settings.InitialDirectory = string.Join('\\', pathSegments.SkipLast(1));

        string? fileName = pathSegments.LastOrDefault();

        if (fileName is not null)
        {
            settings.DefaultExtension = fileName.Split('.').LastOrDefault();
        }

        settings.FileName = fileName;

        return SaveFile(settings);
    }
    /// <exception cref="ArgumentNullException"/>
    public string? SaveFile(SaveFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return InvokeShowDialogOnCommonSaveFileDialog(commonSaveFileDialog =>
        {
            if (commonSaveFileDialog.Title is not null)
            {
                commonSaveFileDialog.Title = settings.Title;
            }

            if (commonSaveFileDialog.DefaultFileName is not null)
            {
                commonSaveFileDialog.DefaultFileName = settings.FileName;
            }

            if (settings.InitialDirectory is not null)
            {
                commonSaveFileDialog.InitialDirectory = settings.InitialDirectory;
            }

            if (settings.DefaultExtension is not null)
            {
                commonSaveFileDialog.DefaultExtension = settings.DefaultExtension;
                commonSaveFileDialog.Filters.Add(new CommonFileDialogFilter(settings.DefaultExtension, settings.DefaultExtension));
            }

            if (settings.EnsureValidNames is not null)
            {
                commonSaveFileDialog.EnsureValidNames = settings.EnsureValidNames.Value;
            }
        });
    }

    public string? OpenFile() => OpenFile(new OpenFileSettings());
    /// <exception cref="ArgumentNullException"/>
    public string? OpenFile(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return OpenFile(new OpenFileSettings
        {
            InitialDirectory = initalDirectoryPath,
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public string? OpenFile(OpenFileSettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            if (commonOpenFileDialog.Title is not null)
            {
                commonOpenFileDialog.Title = settings.Title;
            }

            if (commonOpenFileDialog.DefaultFileName is not null)
            {
                commonOpenFileDialog.DefaultFileName = settings.FileName;
            }

            if (settings.InitialDirectory is not null)
            {
                commonOpenFileDialog.InitialDirectory = settings.InitialDirectory;
            }

            if (settings.DefaultExtension is not null)
            {
                commonOpenFileDialog.DefaultExtension = settings.DefaultExtension;
                commonOpenFileDialog.Filters.Add(new CommonFileDialogFilter(settings.DefaultExtension, settings.DefaultExtension));
            }

            if (settings.EnsureFileExists is not null)
            {
                commonOpenFileDialog.EnsureFileExists = settings.EnsureFileExists.Value;
            }

            if (settings.EnsurePathExists is not null)
            {
                commonOpenFileDialog.EnsurePathExists = settings.EnsurePathExists.Value;
            }
        });
    }

    public string? SelectDirectory() => SelectDirectory(new SelectDirectorySettings());
    /// <exception cref="ArgumentNullException"/>
    public string? SelectDirectory(string initalDirectoryPath)
    {
        ArgumentNullException.ThrowIfNull(initalDirectoryPath);

        return SelectDirectory(new SelectDirectorySettings
        {
            InitialDirectory = initalDirectoryPath,
        });
    }
    /// <exception cref="ArgumentNullException"/>
    public string? SelectDirectory(SelectDirectorySettings settings)
    {
        ArgumentNullException.ThrowIfNull(settings);

        return InvokeShowDialogOnCommonOpenFileDialog(commonOpenFileDialog =>
        {
            commonOpenFileDialog.IsFolderPicker = true;
            commonOpenFileDialog.Title = settings.Title;
            commonOpenFileDialog.InitialDirectory = settings.InitialDirectory;
        });
    }
}
