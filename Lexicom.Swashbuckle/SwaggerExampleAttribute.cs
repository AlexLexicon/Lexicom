namespace Lexicom.Swashbuckle;
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
public class SwaggerExampleAttribute : Attribute
{
    /// <exception cref="ArgumentNullException"/>
    public SwaggerExampleAttribute(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        Json = json;
    }

    public string Json { get; }
}
