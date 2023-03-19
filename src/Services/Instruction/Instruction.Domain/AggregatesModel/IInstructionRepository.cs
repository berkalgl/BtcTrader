using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public interface IInstructionRepository : IRepository<Instruction>
    {
        Task<Instruction> AddAsync(Instruction instruction);
        Task<Instruction?> GetActiveByAsync(int userId);
    }
}
