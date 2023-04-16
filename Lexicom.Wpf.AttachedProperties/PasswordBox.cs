using System.Windows;
using System.Windows.Input;

namespace Lexicom.Wpf.AttachedProperties;
public static class PasswordBox
{
    /*
     * Password
     */
    public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBox), new PropertyMetadata(default, OnPasswordChanged, OnPassword_PasswordBox_CoerceValueCallback));
    public static string? GetPassword(DependencyObject obj) => (string?)obj.GetValue(PasswordProperty);
    public static void SetPassword(DependencyObject obj, string? value) => obj.SetValue(PasswordProperty, value);
    private static void OnPasswordChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Controls.PasswordBox passwordBox)
        {
            passwordBox.PasswordChanged -= OnPasswordChanged_PasswordBox_PasswordChanged;
            passwordBox.PasswordChanged += OnPasswordChanged_PasswordBox_PasswordChanged;

            if (args.NewValue is null or string)
            {
                string? password = args.NewValue is string argsPassword ? argsPassword : null;
                if (password != passwordBox.Password)
                {
                    passwordBox.Password = password;
                }
            }
        }
    }
    private static void OnPasswordChanged_PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is System.Windows.Controls.PasswordBox passwordBox)
        {
            SetPassword(passwordBox, passwordBox.Password);
        }
    }
    private static object? OnPassword_PasswordBox_CoerceValueCallback(DependencyObject d, object baseValue)
    {
        if (d is System.Windows.Controls.PasswordBox passwordBox)
        {
            OnPasswordChanged(d, new DependencyPropertyChangedEventArgs(PasswordProperty, passwordBox.Password, baseValue));
        }

        return baseValue;
    }

    /*
     * EnterKeyPressedCommand
     */
    public static readonly DependencyProperty EnterKeyPressedCommandProperty = DependencyProperty.RegisterAttached("EnterKeyPressedCommand", typeof(ICommand), typeof(PasswordBox), new PropertyMetadata(OnEnterKeyPressedCommand_PasswordBox_PropertyChanged));
    public static ICommand? GetEnterKeyPressedCommand(DependencyObject obj) => (ICommand?)obj.GetValue(EnterKeyPressedCommandProperty);
    public static void SetEnterKeyPressedCommand(DependencyObject obj, ICommand? value) => obj.SetValue(EnterKeyPressedCommandProperty, value);
    private static void OnEnterKeyPressedCommand_PasswordBox_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Controls.PasswordBox passwordBox)
        {
            passwordBox.PreviewKeyDown -= OnEnterKeyPressedCommand_PasswordBox_PreviewKeyDown;
            passwordBox.PreviewKeyDown += OnEnterKeyPressedCommand_PasswordBox_PreviewKeyDown;
        }
    }
    private static void OnEnterKeyPressedCommand_PasswordBox_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (sender is System.Windows.Controls.PasswordBox passwordBox && e.Key == Key.Enter)
        {
            ICommand? enterKeyPressedCommand = GetEnterKeyPressedCommand(passwordBox);
            enterKeyPressedCommand?.Execute(null);
        }
    }
}
