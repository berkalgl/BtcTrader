using Instruction.Domain.AggregatesModel;
using Instruction.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace Instruction.Infrastructure.Repositories
{
    public class InstructionRepository : IInstructionRepository
    {
        private readonly InstructionDbContext _dbContext;
        public IUnitOfWork UnitOfWork => _dbContext;
        public InstructionRepository(InstructionDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Domain.AggregatesModel.Instruction> AddAsync(Domain.AggregatesModel.Instruction instruction)
        {
            var added = await _dbContext.Instructions.AddAsync(instruction);
            return added.Entity;
        }
        public async Task<Domain.AggregatesModel.Instruction?> GetActiveByAsync(int userId)
        {
            var instruction = await _dbContext.Instructions
                .Include(i => i.Notifications)
                .FirstOrDefaultAsync(i => i.UserId.Equals(userId) && i.StatusId.Equals(Status.Active.Id));

            return instruction;
        }
    }
}
