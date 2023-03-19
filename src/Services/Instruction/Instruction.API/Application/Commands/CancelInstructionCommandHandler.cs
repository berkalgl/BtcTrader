using Instruction.Domain.Services;
using MediatR;

namespace Instruction.API.Application.Commands
{
    public class CancelInstructionCommandHandler : IRequestHandler<CancelInstructionCommand, bool>
    {
        private readonly IInstructionService _instructionService;
        public CancelInstructionCommandHandler(IInstructionService instructionService)
        {
            _instructionService = instructionService;
        }
        public async Task<bool> Handle(CancelInstructionCommand request, CancellationToken cancellationToken)
        {
            var instruction = await _instructionService.CancelAsync(request.UserId, cancellationToken);
            return instruction is not null;
        }
    }
}
