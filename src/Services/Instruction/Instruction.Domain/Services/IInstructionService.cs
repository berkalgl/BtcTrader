using Instruction.Domain.AggregatesModel;

namespace Instruction.Domain.Services
{
    public interface IInstructionService
    {
        Task<AggregatesModel.Instruction> AddAsync(AggregatesModel.Instruction instruction, CancellationToken cancellationToken);
        Task<AggregatesModel.Instruction?> CancelAsync(int userId, CancellationToken cancellationToken);
        Task<AggregatesModel.Instruction?> GetActiveByAsync(int userId);
        Task<List<NotificationEnum>> GetNotificationsAsync(int userId);
    }
}
