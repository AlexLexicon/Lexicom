using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lexicom.Wpf.Controls;
public partial class ButtonForegroundImage : Label
{
    public ButtonForegroundImage() => InitializeComponent();

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof(ImageSource), typeof(ImageSource), typeof(ButtonForegroundImage));
    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }
}
