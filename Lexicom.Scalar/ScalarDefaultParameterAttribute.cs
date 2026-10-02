namespace Lexicom.Scalar;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class ScalarDefaultParameterAttribute : Attribute
{
    /// <exception cref="ArgumentNullException"/>
    public ScalarDefaultParameterAttribute(
        string paramName,
        object? defaultValue,
        bool isRequired = true)
    {
        ArgumentNullException.ThrowIfNull(paramName);

        ParamName = paramName;
        DefaultValue = defaultValue;
        IsRequired = isRequired;
    }

    public string ParamName { get; }
    public object? DefaultValue { get; }
    public bool IsRequired { get; }
}
