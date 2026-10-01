namespace Lexicom.Authentication.Options;

public class ApiKeyOptions
{
    public IList<ApiKeyOptionsDescriptor?>? KeyDescriptions { get; set; }
}
public class ApiKeyOptionsDescriptor
{
    public string? Key { get; set; }
    public IList<string?>? Permissions { get; set; }
}
