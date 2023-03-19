using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public class NotificationItem : Entity
    {
        public int InstructionId { get; private set; }
        public Instruction Instruction { get; private set; }
        public int NotificationId { get; private set; }
        public NotificationEnum Notification => NotificationEnum.From(NotificationId);

        protected NotificationItem() { }
        public NotificationItem(int instructionId, int notificationId)
        {
            InstructionId = instructionId;
            NotificationId = notificationId;
        }
    }
}
