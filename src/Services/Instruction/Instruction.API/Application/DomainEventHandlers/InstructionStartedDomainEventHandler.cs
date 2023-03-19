using Instruction.Domain.Events;
using MassTransit;
using MediatR;
using MessagesAndEvents.Enums;
using MessagesAndEvents.Events.V1;

namespace Instruction.API.Application.DomainEventHandlers
{
    public class InstructionStartedDomainEventHandler : INotificationHandler<InstructionStartedDomainEvent>
    {
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly ILogger<InstructionStartedDomainEventHandler> _logger;

        public InstructionStartedDomainEventHandler(IPublishEndpoint publishEndPoint, ILogger<InstructionStartedDomainEventHandler> logger)
        {
            _publishEndPoint = publishEndPoint;
            _logger = logger;
        }

        public async Task Handle(InstructionStartedDomainEvent message, CancellationToken cancellationToken)
        {
            var instruction = message.Instruction;
            var messageToSent = $"{instruction.Id} numaralı Bitcoin alım talebiniz alınmıştır.";

            foreach(var notification in instruction.Notifications)
            {
                _logger.LogInformation("Sending Notification");
                var notificationCreatedEvent = new NotificationCreatedEvent(messageToSent, (NotificationTypeEnum)notification.NotificationId);
                await _publishEndPoint.Publish(notificationCreatedEvent, cancellationToken);
            }
        }
    }
}
