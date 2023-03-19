using Instruction.Domain.Exceptions;
using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public class Status : Enumeration
    {
        public static Status Active = new(1, nameof(Active));
        public static Status Cancelled = new(2, nameof(Cancelled));
        public Status(int id, string name) : base(id, name)
        {
        }
        public static IEnumerable<Status> List() =>
            new[] { Active, Cancelled };
        public static Status FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new InstructionDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static Status From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new InstructionDomainException($"Possible values for Status: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
