namespace Notification.Consumer.Services.Providers
{
    internal class EmailOperation : INotificationOperation
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"Sending email with message {message}");
            return Task.CompletedTask;
        }
    }
}
