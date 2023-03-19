namespace Notification.Consumer.Services.Providers
{
    internal class PushOperation : INotificationOperation
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"Sending push notification with message {message}");
            return Task.CompletedTask;
        }
    }
}
