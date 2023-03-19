using Instruction.API.Application.DTOs;
using Instruction.API.Application.Enums;

namespace Instruction.API.Application.Queries
{
    public interface IInstructionQueries
    {
        Task<InstructionDTO?> GetActiveAsync(int userId);
        Task<List<InstructionNotification>> GetNotificationsAsync(int userId);
    }
}
