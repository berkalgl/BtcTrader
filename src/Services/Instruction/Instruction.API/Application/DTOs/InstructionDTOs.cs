using Instruction.API.Application.Enums;

namespace Instruction.API.Application.DTOs
{
    public record InstructionDTO
    {
        public static InstructionDTO FromInstruction(Domain.AggregatesModel.Instruction instruction)
        {
            return new InstructionDTO
            {
                Id = instruction.Id,
                TransactionDay = instruction.TransactionDay,
                UserId = instruction.UserId,
                Amount = instruction.Price.Amount
            };
        }
        public int Id { get; private set; }
        public int TransactionDay { get; private set; }
        public int UserId { get; private set; }
        public decimal Amount { get; set; }
    }

    public record CreateInstructionDTO
    {
        public int TransactionDay { get; set; }
        public decimal Amount { get; set; }
        public HashSet<InstructionNotification> Notifications { get; set; } = new HashSet<InstructionNotification>();
    }
}
