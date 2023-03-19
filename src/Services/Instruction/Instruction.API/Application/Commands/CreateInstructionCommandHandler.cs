using Instruction.API.Application.DTOs;
using Instruction.Domain.AggregatesModel;
using Instruction.Domain.Services;
using MediatR;

namespace Instruction.API.Application.Commands
{
    public class CreateInstructionCommandHandler : IRequestHandler<CreateInstructionCommand, InstructionDTO>
    {
        private readonly IInstructionService _instructionService;
        public CreateInstructionCommandHandler(IInstructionService instructionService)
        {
            _instructionService = instructionService;
        }
        public async Task<InstructionDTO> Handle(CreateInstructionCommand request, CancellationToken cancellationToken)
        {
            var instruction = new Domain.AggregatesModel.Instruction(
                request.TransactionDay, 
                new Price(request.Amount), 
                request.UserId
                );

            foreach ( var notification in request.Notifications )
            {
                instruction.AddNotification(NotificationEnum.FromName(notification.ToString()).Id);
            }

            await _instructionService.AddAsync(instruction, cancellationToken);

            return InstructionDTO.FromInstruction(instruction);
        }
    }
}
