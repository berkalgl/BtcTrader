using MessagesAndEvents.Enums;

namespace MessagesAndEvents.Events.V1
{
    public record NotificationCreatedEvent
    {
        public string Message { get; }

        public NotificationTypeEnum Type { get; }

        public NotificationCreatedEvent(string message, NotificationTypeEnum type)
        {
            Message = message;
            Type = type;
        }
    }
}
