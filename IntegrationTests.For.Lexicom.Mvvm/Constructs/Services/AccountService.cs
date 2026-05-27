using IntegrationTests.For.Lexicom.Mvvm.Constructs.Models;

namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;

public interface IAccountService
{
    Task<Account> GetLoggedInAccountAsync();
    Task<string> GetProfileNameAsync(Guid profileId);
}
public class AccountService : IAccountService
{
    public Task<Account> GetLoggedInAccountAsync()
    {
        var account = new Account
        {
            ProfileId = Guid.NewGuid(),
        };

        return Task.FromResult(account);
    }

    public Task<string> GetProfileNameAsync(Guid profileId)
    {
        return Task.FromResult("Alex");
    }
}
