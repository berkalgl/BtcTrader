using Instruction.Domain.Events;
using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public class Instruction : Entity, IAggregateRoot
    {
        public int TransactionDay { get; private set; }
        public Price Price { get; private set; }
        public int UserId { get; private set; }
        public List<NotificationItem> Notifications{ get; private set; }
        public Status Status => Status.From(Id);
        public int StatusId { get; private set; }

        protected Instruction()
        {
            Notifications = new List<NotificationItem>();
            Price = new Price();
            StatusId = Status.Active.Id;

        }

        public Instruction(int transactionDay, Price price, int userId) : this()
        {
            TransactionDay = transactionDay;
            Price = price;
            UserId = userId;

            AddInstructionStartedDomainEvent();
        }
        public Instruction AddNotification(int notificationEnumId)
        {
            Notifications.Add(new NotificationItem(Id, notificationEnumId));
            return this;
        }
        public Instruction Cancel()
        {
            StatusId = Status.Cancelled.Id;
            return this;
        }
        void AddInstructionStartedDomainEvent()
        {
            var instructionStartedDomainEvent = new InstructionStartedDomainEvent(this);
            AddDomainEvent(instructionStartedDomainEvent);
        }
    }
}
