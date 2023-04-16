using System.Windows;

namespace Lexicom.Wpf.AttachedProperties;
public static class Window
{
    /*
     * IsMaximized
     */
    public static readonly DependencyProperty IsMaximizedProperty = DependencyProperty.RegisterAttached("IsMaximized", typeof(bool), typeof(Window), new PropertyMetadata(false, OnIsMaximized_Window_PropertyChanged, OnIsMaximized_Window_CoerceValueCallback));
    public static bool GetIsMaximized(DependencyObject obj) => (bool)obj.GetValue(IsMaximizedProperty);
    public static void SetIsMaximized(DependencyObject obj, bool value) => obj.SetValue(IsMaximizedProperty, value);
    private static void OnIsMaximized_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool newIsMaximized)
        {
            window.SizeChanged -= OnIsMaximized_Window_SizeChanged;
            window.SizeChanged += OnIsMaximized_Window_SizeChanged;

            bool currentIsMaximized = window.WindowState == WindowState.Maximized;

            if (currentIsMaximized != newIsMaximized)
            {
                window.WindowState = newIsMaximized ? WindowState.Maximized : WindowState.Normal;
            }
        }
    }
    private static void OnIsMaximized_Window_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is System.Windows.Window window)
        {
            bool previousIsMaximized = GetIsMaximized(window);
            bool currentIsMaximized = window.WindowState == WindowState.Maximized;

            if (currentIsMaximized != previousIsMaximized)
            {
                SetIsMaximized(window, currentIsMaximized);
            }
        }
    }
    private static object? OnIsMaximized_Window_CoerceValueCallback(DependencyObject d, object baseValue)
    {
        if (d is System.Windows.Window window)
        {
            OnIsMaximized_Window_PropertyChanged(window, new DependencyPropertyChangedEventArgs(IsMaximizedProperty, window.WindowState == WindowState.Maximized, baseValue));
        }

        return baseValue;
    }

    /*
     * ShowAction
     */
    public static readonly DependencyProperty ShowActionProperty = DependencyProperty.RegisterAttached("ShowAction", typeof(Action), typeof(Window), new PropertyMetadata(OnShowAction_Window_PropertyChanged));
    public static Action GetShowAction(DependencyObject obj) => (Action)obj.GetValue(ShowActionProperty);
    public static void SetShowAction(DependencyObject obj, Action value) => obj.SetValue(ShowActionProperty, value);
    private static void OnShowAction_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is Action)
        {
            SetShowAction(window, window.Show);
        }
    }

    /*
     * CloseAction
     */
    public static readonly DependencyProperty CloseActionProperty = DependencyProperty.RegisterAttached("CloseAction", typeof(Action), typeof(Window), new PropertyMetadata(OnCloseAction_Window_PropertyChanged));
    public static Action GetCloseAction(DependencyObject obj) => (Action)obj.GetValue(CloseActionProperty);
    public static void SetCloseAction(DependencyObject obj, Action value) => obj.SetValue(CloseActionProperty, value);
    private static void OnCloseAction_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is Action)
        {
            SetCloseAction(window, window.Close);
        }
    }

    /*
     * HideAction
     */
    public static readonly DependencyProperty HideActionProperty = DependencyProperty.RegisterAttached("HideAction", typeof(Action), typeof(Window), new PropertyMetadata(OnHideAction_Window_PropertyChanged));
    public static Action GetHideAction(DependencyObject obj) => (Action)obj.GetValue(CloseActionProperty);
    public static void SetHideAction(DependencyObject obj, Action value) => obj.SetValue(CloseActionProperty, value);
    private static void OnHideAction_Window_PropertyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is bool isInvoked && isInvoked)
        {
            SetHideAction(window, window.Hide);
        }
    }

    /*
     * ShowDialogAction
     */
    public static readonly DependencyProperty ShowDialogActionProperty = DependencyProperty.RegisterAttached("ShowDialogAction", typeof(Action), typeof(Window), new PropertyMetadata(OnShowDialogAction_Window_ProeprtyChanged));
    public static Action GetShowDialogAction(DependencyObject obj) => (Action)obj.GetValue(ShowDialogActionProperty);
    public static void SetShowDialogAction(DependencyObject obj, Action value) => obj.SetValue(ShowDialogActionProperty, value);
    private static void OnShowDialogAction_Window_ProeprtyChanged(object sender, DependencyPropertyChangedEventArgs args)
    {
        if (sender is System.Windows.Window window && args.NewValue is Action)
        {
            SetShowDialogAction(window, () => window.ShowDialog());
        }
    }
}
