namespace Lexicom.Authentication.Options;

public class ApiKeyOptions
{
    public IList<ApiKeyOptionsKey>? Keys { get; set; }
}
public class ApiKeyOptionsKey
{
    public string? Key { get; set; }

    //the Id value becomes the 'sub' claim
    public Guid? Id { get; set; }
    //the Permissions values become 'permission' claims
    public IList<string>? Permissions { get; set; }
    //the Roles become values 'role' claims
    public IList<string>? Roles { get; set; }
    public IDictionary<string, string?>? Claims { get; set; }
}
