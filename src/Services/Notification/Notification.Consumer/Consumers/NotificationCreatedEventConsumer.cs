using MassTransit;
using MessagesAndEvents.Events.V1;
using Notification.Consumer.Strategies;
using Notification.Domain.Services;

namespace Notification.Consumer.Consumers
{
    public class NotificationCreatedEventConsumer : IConsumer<NotificationCreatedEvent>
    {
        private readonly INotificationProviderFactory _notificationProviderFactory;
        private readonly INotificationService _notificationService;

        public NotificationCreatedEventConsumer(INotificationProviderFactory notificationProviderFactory, INotificationService notificationService)
        {
            _notificationProviderFactory = notificationProviderFactory;
            _notificationService = notificationService;
        }

        public async Task Consume(ConsumeContext<NotificationCreatedEvent> context)
        {
            var provider = _notificationProviderFactory.GetNotificationProvider(context.Message.Type);
            await provider.SendAsync(context.Message.Message);
            await _notificationService
                .AddAsync(new Domain.AggregatesModel.Notification(context.Message.Message, context.Message.Type.ToString()), context.CancellationToken);
        }
    }
}
