using System.Windows;
using System.Windows.Input;

namespace Lexicom.Wpf.AttachedProperties;
public static class TextBox
{
    /*
     * IsAllSelected
     */
    public static readonly DependencyProperty IsAllSelectedProperty = DependencyProperty.RegisterAttached("IsSelectedAll", typeof(bool), typeof(TextBox), new FrameworkPropertyMetadata(OnIsAllSelected_TextBox_PropertyChanged)
    {
        BindsTwoWayByDefault = true
    });
    public static bool GetIsAllSelected(DependencyObject obj) => (bool)obj.GetValue(IsAllSelectedProperty);
    public static void SetIsAllSelected(DependencyObject obj, bool isAllSelected) => obj.SetValue(IsAllSelectedProperty, isAllSelected);
    private static void OnIsAllSelected_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox && GetIsAllSelected(textBox))
        {
            textBox.Focus();
            textBox.SelectAll();
        }
    }

    /*
     * EnterKeyPressedCommand
     */
    public static readonly DependencyProperty EnterKeyPressedCommandProperty = DependencyProperty.RegisterAttached("EnterKeyPressedCommand", typeof(ICommand), typeof(TextBox), new PropertyMetadata(OnEnterKeyPressedCommand_TextBox_PropertyChanged));
    public static ICommand? GetEnterKeyPressedCommand(DependencyObject obj) => (ICommand?)obj.GetValue(EnterKeyPressedCommandProperty);
    public static void SetEnterKeyPressedCommand(DependencyObject obj, ICommand? value) => obj.SetValue(EnterKeyPressedCommandProperty, value);
    private static void OnEnterKeyPressedCommand_TextBox_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Controls.TextBox textBox)
        {
            textBox.PreviewKeyDown -= OnEnterKeyPressedCommand_TextBox_PreviewKeyDown;
            textBox.PreviewKeyDown += OnEnterKeyPressedCommand_TextBox_PreviewKeyDown;
        }
    }
    private static void OnEnterKeyPressedCommand_TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is System.Windows.Controls.TextBox textBox && e.Key == Key.Enter)
        {
            ICommand? enterPressedCommand = GetEnterKeyPressedCommand(textBox);
            enterPressedCommand?.Execute(null);
        }
    }

    /*
     * InputBindings
     */
    public static readonly DependencyProperty InputBindingsProperty = DependencyProperty.RegisterAttached("InputBindings", typeof(InputBindingCollection), typeof(TextBox), new FrameworkPropertyMetadata(new InputBindingCollection(), OnInputBindings_TextBox_PropertyChanged));
    public static InputBindingCollection? GetInputBindings(DependencyObject obj) => (InputBindingCollection?)obj.GetValue(InputBindingsProperty);
    public static void SetInputBindings(DependencyObject obj, InputBindingCollection? value) => obj.SetValue(InputBindingsProperty, value);
    private static void OnInputBindings_TextBox_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is not null and UIElement element)
        {
            element.InputBindings.Clear();
            element.InputBindings.AddRange((InputBindingCollection)args.NewValue);
        }
    }
}
