using Instruction.API.Application.DTOs;
using Instruction.API.Application.Enums;
using Instruction.Domain.Services;

namespace Instruction.API.Application.Queries
{
    public class InstructionQueries : IInstructionQueries
    {
        private readonly IInstructionService _instructionService;
        public InstructionQueries(IInstructionService instructionService)
        {
            _instructionService = instructionService;
        }
        public async Task<InstructionDTO?> GetActiveAsync(int userId)
        {
            var instruction = await _instructionService.GetActiveByAsync(userId);

            return instruction is null ? null : InstructionDTO.FromInstruction(instruction);
        }
        public async Task<List<InstructionNotification>> GetNotificationsAsync(int userId)
        {
            var notifications = await _instructionService.GetNotificationsAsync(userId);

            return notifications.Select(n => (InstructionNotification)Enum.Parse(typeof(InstructionNotification), n.Name)).ToList();
        }
    }
}
