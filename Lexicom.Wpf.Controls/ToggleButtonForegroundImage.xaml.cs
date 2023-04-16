using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lexicom.Wpf.Controls;
public partial class ToggleButtonForegroundImage : Label
{
    public ToggleButtonForegroundImage() => InitializeComponent();

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ToggleButtonForegroundImage));
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}
