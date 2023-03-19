namespace Notification.Consumer.Services.Providers
{
    internal class SmsOperation : INotificationOperation
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"Sending sms with message {message}");
            return Task.CompletedTask;
        }
    }
}
