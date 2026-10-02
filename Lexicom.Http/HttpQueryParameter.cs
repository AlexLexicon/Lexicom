using Lexicom.Http.Exceptions;
using System.Globalization;

namespace Lexicom.Http;

public class HttpQueryParameter
{
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        short value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        int value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        long value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        float value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        double value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        decimal value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        uint value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        ulong value)
        : this(name, value.ToString(CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        Guid value)
        : this(name, value.ToString())
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        DateTime value)
        : this(name, value.ToString("o", CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        DateTimeOffset value)
        : this(name, value.ToString("o", CultureInfo.InvariantCulture))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        object value)
        : this(name, ConvertValueToInvariantString(value))
    {
    }
    /// <exception cref="ArgumentNullException"/>
    ///<exception cref="InvalidHttpQueryParameterNameException"/>
    public HttpQueryParameter(
        string name,
        string value)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(value);

        //the name is invalid if the name is empty or if the
        //name contains characters that would need to be escaped
        if (string.IsNullOrWhiteSpace(name) || name != Uri.EscapeDataString(name))
        {
            throw new InvalidHttpQueryParameterNameException(name);
        }

        Name = name;
        Value = value;
    }

    public string Name { get; }
    public string Value { get; }

    public string EscapedValue => field ??= Uri.EscapeDataString(Value);

    public override string ToString()
    {
        return $"{Name}={EscapedValue}";
    }

    /// <exception cref="ArgumentNullException"/>
    private static string ConvertValueToInvariantString(object value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return Convert.ToString(value, CultureInfo.InvariantCulture) ?? string.Empty;
    }
}
