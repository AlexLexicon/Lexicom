using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace Lexicom.Wpf.AttachedProperties;
public static class Hyperlink
{
    /*
     * IsProcessStartUri
     */
    public static readonly DependencyProperty IsProcessStartUriProperty = DependencyProperty.RegisterAttached("IsProcessStartUri", typeof(bool), typeof(Hyperlink), new UIPropertyMetadata(false, OnIsProcessStartUri_Hyperlink_PropertyChanged));
    public static bool GetIsProcessStartUri(DependencyObject obj) => (bool)obj.GetValue(IsProcessStartUriProperty);
    public static void SetIsProcessStartUri(DependencyObject obj, bool value) => obj.SetValue(IsProcessStartUriProperty, value);
    private static void OnIsProcessStartUri_Hyperlink_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Documents.Hyperlink hyperlink)
        {
            if (args.NewValue is not null && args.NewValue is bool value && value)
            {
                hyperlink.RequestNavigate += OnIsProcessStartUri_Hyperlink_RequestNavigate;
            }
            else
            {
                hyperlink.RequestNavigate -= OnIsProcessStartUri_Hyperlink_RequestNavigate;
            }
        }
    }
    private static void OnIsProcessStartUri_Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri)
        {
            UseShellExecute = true
        });

        e.Handled = true;
    }
}
