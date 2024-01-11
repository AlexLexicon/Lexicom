using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lexicom.Wpf.AttachedProperties;
public static class TextBox
{
    private static HashSet<int> HashCodesOfTextBoxesCurrentlyBeingValidated { get; } = [];

    /*
     * Validation
     */
    public static readonly DependencyProperty ValidationProperty = DependencyProperty.RegisterAttached("Validation", typeof(Func<string?, IEnumerable<string?>>), typeof(TextBox), new FrameworkPropertyMetadata(null, OnValidationProperty_TextBox_PropertyChanged));
    public static Func<string?, IEnumerable<string?>>? GetValidation(DependencyObject obj) => (Func<string?, IEnumerable<string?>>?)obj.GetValue(ValidationProperty);
    public static void SetValidation(DependencyObject obj, Func<string?, IEnumerable<string?>>? validation) => obj.SetValue(ValidationProperty, validation);
    private static void OnValidationProperty_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;

            Validate(textBox);
        }
    }

    /*
     * IsValid
     */
    public static readonly DependencyProperty IsValidProperty = DependencyProperty.RegisterAttached("IsValid", typeof(bool), typeof(TextBox), new FrameworkPropertyMetadata(true, OnIsValidProperty_TextBox_PropertyChanged));
    public static bool GetIsValid(DependencyObject obj) => (bool)obj.GetValue(IsValidProperty);
    public static void SetIsValid(DependencyObject obj, bool isValid) => obj.SetValue(IsValidProperty, isValid);
    private static void OnIsValidProperty_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;

            Validate(textBox);
        }
    }

    /*
     * Errors
     */
    public static readonly DependencyProperty ErrorsProperty = DependencyProperty.RegisterAttached("Errors", typeof(IEnumerable<string?>), typeof(TextBox), new FrameworkPropertyMetadata(null, OnValidationProperty_TextBox_PropertyChanged));
    public static IEnumerable<string?>? GetErrors(DependencyObject obj) => (IEnumerable<string?>?)obj.GetValue(ErrorsProperty);
    public static void SetErrors(DependencyObject obj, IEnumerable<string?>? errors) => obj.SetValue(ErrorsProperty, errors);
    private static void OnErrorsProperty_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs _)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;

            Validate(textBox);
        }
    }

    /*
     * ValidateCommand
     */
    public static readonly DependencyProperty ValidateCommandProperty = DependencyProperty.RegisterAttached("ValidateCommand", typeof(ICommand), typeof(TextBox), new FrameworkPropertyMetadata(null, OnValidateCommandProperty_TextBox_PropertyChanged));
    public static ICommand? GetValidateCommand(DependencyObject obj) => (ICommand?)obj.GetValue(ValidateCommandProperty);
    public static void SetValidateCommand(DependencyObject obj, ICommand? command) => obj.SetValue(ValidateCommandProperty, command);
    private static void OnValidateCommandProperty_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;

            Validate(textBox);
        }
    }

    /*
     * ValidateCommandParameter
     */
    public static readonly DependencyProperty ValidateCommandParameterProperty = DependencyProperty.RegisterAttached("ValidateCommandParameter", typeof(object), typeof(TextBox), new FrameworkPropertyMetadata(null, OnValidateCommandParameterProperty_TextBox_PropertyChanged));
    public static object? GetValidateCommandParameter(DependencyObject obj) => (object?)obj.GetValue(ValidateCommandParameterProperty);
    public static void SetValidateCommandParameter(DependencyObject obj, object? parameter) => obj.SetValue(ValidateCommandParameterProperty, parameter);
    private static void OnValidateCommandParameterProperty_TextBox_PropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        if (dependencyObject is System.Windows.Controls.TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;
            textBox.TextChanged += TextBox_TextChanged;

            Validate(textBox);
        }
    }

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

    private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        Validate(sender);
    }

    private static void Validate(object obj)
    {
        if (obj is System.Windows.Controls.TextBox textBox)
        {
            int hashCode = textBox.GetHashCode();
            if (!HashCodesOfTextBoxesCurrentlyBeingValidated.Contains(hashCode))
            {
                HashCodesOfTextBoxesCurrentlyBeingValidated.Add(hashCode);

                Func<string?, IEnumerable<string?>>? validation = GetValidation(textBox);

                IEnumerable<string?>? errors = validation?.Invoke(textBox.Text);
                bool isValid = errors is not null && !errors.Any();

                SetErrors(textBox, errors);
                SetIsValid(textBox, isValid);

                ICommand? command = GetValidateCommand(textBox);
                object? parameter = GetValidateCommandParameter(textBox);

                command?.Execute(parameter);

                HashCodesOfTextBoxesCurrentlyBeingValidated.Remove(hashCode);
            }
        }
    }
}
