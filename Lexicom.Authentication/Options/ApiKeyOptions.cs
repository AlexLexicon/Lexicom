namespace Lexicom.Authentication.Options;

public class ApiKeyOptions
{
    public IList<ApiKeyOptionsKey>? Keys { get; set; }
}
public class ApiKeyOptionsKey
{
    public string? Key { get; set; }

    //The Id become value the 'sub' claim
    public Guid? Id { get; set; }
    //The Permissions values become 'permission' claims
    public IList<string>? Permissions { get; set; }
    //The Roles become values 'role' claims
    public IList<string>? Roles { get; set; }
    public IDictionary<string, string>? Claims { get; set; }
}
