using Microsoft.WindowsAPICodePack.Dialogs;

namespace Lexicom.Wpf.Amenities.Exceptions;
public class CommonFileDialogMissingFilterException(Exception? innerException) : Exception($"The {nameof(CommonFileDialog)} requires a file extension filter. Consider using Settings.DefaultExtension or adding a filter.", innerException)
{
}
