using MediatR;

namespace Instruction.API.Application.Commands
{
    public class CancelInstructionCommand : IRequest<bool>
    {
        public int UserId { get; private set; }

        public CancelInstructionCommand(int userId)
        {
            UserId = userId;
        }

    }
}
