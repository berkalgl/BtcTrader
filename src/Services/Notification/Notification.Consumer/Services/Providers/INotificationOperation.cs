namespace Notification.Consumer.Services.Providers
{
    public interface INotificationOperation
    {
        Task SendAsync(string message);
    }
}
