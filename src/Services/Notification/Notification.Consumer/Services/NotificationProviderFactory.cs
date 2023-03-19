using MessagesAndEvents.Enums;
using Notification.Consumer.Services.Providers;
using Notification.Consumer.Strategies;

namespace Notification.Consumer.Services
{
    internal class NotificationProviderFactory : INotificationProviderFactory
    {
        public INotificationOperation GetNotificationProvider(NotificationTypeEnum type)
        {
            switch (type)
            {
                case NotificationTypeEnum.Sms: return new SmsOperation();
                case NotificationTypeEnum.Email: return new EmailOperation();
                case NotificationTypeEnum.Push: return new PushOperation();
                default:
                    throw new InvalidOperationException();
            }
        }
    }
}
