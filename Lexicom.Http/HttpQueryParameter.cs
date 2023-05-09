namespace Lexicom.Http;
public class HttpQueryParameter
{
    //these constructors are to avoid boxing common value types
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        int value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        long value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        float value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        double value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        decimal value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        Guid value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        DateTime value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        DateTimeOffset value) : this(name, value.ToString()!)
    {
    }
    /// <exception cref="ArgumentNullException"/>
    public HttpQueryParameter(
        string name,
        object value) :this(name, value?.ToString()!)
    {
    }
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
