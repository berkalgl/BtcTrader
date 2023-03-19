using Instruction.API.Application.DTOs;
using Instruction.API.Application.Enums;
using MediatR;

namespace Instruction.API.Application.Commands
{
    public class CreateInstructionCommand : IRequest<InstructionDTO>
    {
        public int TransactionDay { get; private set; }
        public int UserId { get; private set; }
        public decimal Amount { get; private set; }
        public List<InstructionNotification> Notifications { get; private set; }

        public CreateInstructionCommand(int transactionDay, int userId, decimal amount, HashSet<InstructionNotification> notifications)
        {
            TransactionDay = transactionDay;
            UserId = userId;
            Amount = amount;
            Notifications = notifications.ToList();
        }
    }
}
