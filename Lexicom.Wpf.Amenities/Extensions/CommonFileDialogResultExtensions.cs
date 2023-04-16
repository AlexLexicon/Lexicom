using Lexicom.Wpf.Amenities.Exceptions;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicom.Wpf.Amenities.Extensions;
public static class CommonFileDialogResultExtensions
{
    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="CommonFileDialogMissingFilterException"/>
    public static string? GetFilePathFromShowDialog(this CommonFileDialog commonFileDialog)
    {
        ArgumentNullException.ThrowIfNull(commonFileDialog);

        bool? result = commonFileDialog.ShowDialog() switch
        {
            CommonFileDialogResult.Ok => true,
            CommonFileDialogResult.Cancel => false,
            _ => null,
        };

        if (result is not null && result.Value)
        {
            try
            {
                return commonFileDialog.FileName;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw new CommonFileDialogMissingFilterException(e);
            }
        }

        return null;
    }
}
