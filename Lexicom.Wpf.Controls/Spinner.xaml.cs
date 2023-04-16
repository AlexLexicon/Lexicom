using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Lexicom.Wpf.Controls;
public partial class Spinner : UserControl
{
    public Spinner() => InitializeComponent();

    public static readonly DependencyProperty RingBackgroundProperty = DependencyProperty.Register(nameof(RingBackground), typeof(Brush), typeof(Spinner), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(173, 178, 181))));
    public Brush? RingBackground
    {
        get => (Brush?)GetValue(RingBackgroundProperty);
        set => SetValue(RingBackgroundProperty, value);
    }

    public static readonly DependencyProperty RingForegroundProperty = DependencyProperty.Register(nameof(RingForeground), typeof(Brush), typeof(Spinner), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(126, 180, 234))));
    public Brush? RingForeground
    {
        get => (Brush?)GetValue(RingForegroundProperty);
        set => SetValue(RingForegroundProperty, value);
    }

    public static readonly DependencyProperty RingStrokeThicknessProperty = DependencyProperty.Register(nameof(RingStrokeThickness), typeof(double), typeof(Spinner), new PropertyMetadata(4.0));
    public double RingStrokeThickness
    {
        get => (double)GetValue(RingStrokeThicknessProperty);
        set => SetValue(RingStrokeThicknessProperty, value);
    }
}
