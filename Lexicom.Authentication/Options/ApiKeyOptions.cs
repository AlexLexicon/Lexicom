namespace Lexicom.Authentication.Options;

public class ApiKeyOptions
{
    public IList<ApiKeyOptionsDescriptor>? Keys { get; set; }
}
public class ApiKeyOptionsDescriptor
{
    //used as the 'sub' claim of the authenticated caller
    public Guid? Id { get; set; }
    public string? Key { get; set; }
    //each permission becomes a 'permission' claim
    public IList<string>? Permissions { get; set; }
    //each role becomes a 'role' claim
    public IList<string>? Roles { get; set; }
    //each entry becomes a claim where the key is the claim type
    public IDictionary<string, string>? Claims { get; set; }
}
