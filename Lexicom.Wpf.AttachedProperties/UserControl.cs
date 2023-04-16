using System.Windows;
using System.Windows.Input;

namespace Lexicom.Wpf.AttachedProperties;
public static class UserControl
{
    /*
     * IsVisibleSetToTrueCommand
     */
    public static readonly DependencyProperty IsVisibleSetToTrueCommandProperty = DependencyProperty.RegisterAttached("IsVisibleSetToTrueCommand", typeof(ICommand), typeof(UserControl), new PropertyMetadata(OnIsVisibleSetToTrueCommand_UserControl_PropertyChanged));
    public static ICommand? GetIsVisibleSetToTrueCommand(DependencyObject obj) => (ICommand?)obj.GetValue(IsVisibleSetToTrueCommandProperty);
    public static void SetIsVisibleSetToTrueCommand(DependencyObject obj, ICommand? value) => obj.SetValue(IsVisibleSetToTrueCommandProperty, value);
    private static void OnIsVisibleSetToTrueCommand_UserControl_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Controls.UserControl userControl)
        {
            userControl.IsVisibleChanged -= OnIsVisibleSetToTrueCommand_UserControl_IsVisibleChanged;
            userControl.IsVisibleChanged += OnIsVisibleSetToTrueCommand_UserControl_IsVisibleChanged;

            if (userControl.Visibility == Visibility.Visible)
            {
                ICommand? visibleSetToTrueCommand = GetIsVisibleSetToTrueCommand(userControl);
                visibleSetToTrueCommand?.Execute(null);
            }
        }
    }
    private static void OnIsVisibleSetToTrueCommand_UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is System.Windows.Controls.UserControl userControl && userControl.IsVisible)
        {
            ICommand? visibleSetToTrueCommand = GetIsVisibleSetToTrueCommand(userControl);
            visibleSetToTrueCommand?.Execute(null);
        }
    }

    /*
     * IsVisibleSetToFalseCommand
     */
    public static readonly DependencyProperty IsVisibleSetToFalseCommandProperty = DependencyProperty.RegisterAttached("IsVisibleSetToFalseCommand", typeof(ICommand), typeof(UserControl), new PropertyMetadata(OnIsVisibleSetToFalseCommand_UserControl_PropertyChanged));
    public static ICommand? GetIsVisibleSetToFalseCommand(DependencyObject obj) => (ICommand?)obj.GetValue(IsVisibleSetToFalseCommandProperty);
    public static void SetIsVisibleSetToFalseCommand(DependencyObject obj, ICommand? value) => obj.SetValue(IsVisibleSetToFalseCommandProperty, value);
    private static void OnIsVisibleSetToFalseCommand_UserControl_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Controls.UserControl userControl)
        {
            userControl.IsVisibleChanged -= OnIsVisibleFalse_UserControl_IsVisibleChanged;
            userControl.IsVisibleChanged += OnIsVisibleFalse_UserControl_IsVisibleChanged;

            if (userControl.Visibility != Visibility.Visible)
            {
                ICommand? visibleSetToFalseCommand = GetIsVisibleSetToFalseCommand(userControl);
                visibleSetToFalseCommand?.Execute(null);
            }
        }
    }
    private static void OnIsVisibleFalse_UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (sender is System.Windows.Controls.UserControl userControl && !userControl.IsVisible)
        {
            ICommand? visibleSetToFalseCommand = GetIsVisibleSetToFalseCommand(userControl);
            visibleSetToFalseCommand?.Execute(null);
        }
    }
}
