using Notification.Domain.SeedWork;

namespace Notification.Domain.AggregatesModel
{
    public class Notification : Entity, IAggregateRoot
    {
        public string Message { get; private set; }
        public string Type { get; private set; }
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public Notification(string message, string type)
        {
            Message = message;
            Type = type;
        }
    }
}
