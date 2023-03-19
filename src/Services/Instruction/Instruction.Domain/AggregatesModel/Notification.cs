using Instruction.Domain.Exceptions;
using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public class NotificationEnum : Enumeration
    {
        public static NotificationEnum Sms = new(1, nameof(Sms));
        public static NotificationEnum Email = new(2, nameof(Email));
        public static NotificationEnum Push = new(3, nameof(Push));
        public NotificationEnum(int id, string name) : base(id, name)
        {
        }
        public static IEnumerable<NotificationEnum> List() =>
            new[] { Sms, Email, Push };
        public static NotificationEnum FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new InstructionDomainException($"Possible values for Notification: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static NotificationEnum From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new InstructionDomainException($"Possible values for Notification: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
