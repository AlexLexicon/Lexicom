namespace Lexicom.Authority;
public class BearerToken
{
    public BearerToken(
        Guid jti, 
        DateTimeOffset expires,
        string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        Jti = jti;
        Expires = expires;
        Value = value;
    }

    public Guid Jti { get; }
    public DateTimeOffset Expires { get; }
    public string Value { get; }
}
