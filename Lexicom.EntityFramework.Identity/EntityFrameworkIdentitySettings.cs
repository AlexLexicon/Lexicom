using Microsoft.AspNetCore.Identity;

namespace Lexicom.EntityFramework.Identity;
public class EntityFrameworkIdentitySettings
{
    public bool UseAsyncStores { get; set; } = true;
    public Action<IdentityOptions>? SetupAction { get; set; }
}
