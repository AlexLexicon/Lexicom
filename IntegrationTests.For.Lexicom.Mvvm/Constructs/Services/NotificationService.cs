namespace IntegrationTests.For.Lexicom.Mvvm.Constructs.Services;

public interface INotificationService
{
    Task<int> GetNotificationsCountAsync(Guid profileId);
}
public class NotificationService : INotificationService
{
    public NotificationService()
    {
        Count = 5;
    }

    public int Count { get; set; }

    public Task<int> GetNotificationsCountAsync(Guid profileId)
    {
        return Task.FromResult(Count);
    }
}
