using MediatR;

namespace Instruction.Domain.Events
{
    public record InstructionStartedDomainEvent : INotification
    {
        public AggregatesModel.Instruction Instruction { get; }

        public InstructionStartedDomainEvent(AggregatesModel.Instruction instruction)
        {
            Instruction = instruction;
        }
    }
}
