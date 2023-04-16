namespace Lexicom.Swashbuckle;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class SwaggerParameterAttribute : Attribute
{
    /// <exception cref="ArgumentNullException"/>
    public SwaggerParameterAttribute(
        string paramName, 
        object? defaultValue)
    {
        ArgumentNullException.ThrowIfNull(paramName);

        ParamName = paramName;
        DefaultValue = defaultValue;
    }

    public string ParamName { get; }
    public object? DefaultValue { get; }
}
