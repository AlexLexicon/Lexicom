namespace Lexicom.Http;
public class HttpQueryParameter
{
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name, 
        string value)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(value);

        Name = name;
        Value = value;
    }

    public string Name { get; }
    public string Value { get; }
}
