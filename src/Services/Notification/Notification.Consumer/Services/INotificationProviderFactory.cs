using MessagesAndEvents.Enums;
using Notification.Consumer.Services.Providers;

namespace Notification.Consumer.Strategies
{
    public interface INotificationProviderFactory
    {
        INotificationOperation GetNotificationProvider(NotificationTypeEnum type);
    }
}
