namespace Lexicom.Scalar;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class ScalarDefaultRequestBodyAttribute : Attribute
{
    /// <exception cref="ArgumentNullException"/>
    public ScalarDefaultRequestBodyAttribute(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        Json = json;
    }

    public string Json { get; }
}
