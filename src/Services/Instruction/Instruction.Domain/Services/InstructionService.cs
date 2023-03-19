using Instruction.Domain.AggregatesModel;
using Instruction.Domain.Exceptions;

namespace Instruction.Domain.Services
{
    public class InstructionService : IInstructionService
    {
        private readonly IInstructionRepository _instructionRepository;
        public InstructionService(IInstructionRepository instructionRepository)
        {
            _instructionRepository = instructionRepository;
        }
        public async Task<AggregatesModel.Instruction> AddAsync(AggregatesModel.Instruction instruction, CancellationToken cancellationToken)
        {
            var activeInstruction = await _instructionRepository.GetActiveByAsync(instruction.UserId);

            if (activeInstruction is not null)
                throw new InstructionDomainException("There is already active instruction for this user.");

            await _instructionRepository.AddAsync(instruction);
            await _instructionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return instruction;
        }
        public async Task<AggregatesModel.Instruction?> CancelAsync(int userId, CancellationToken cancellationToken)
        {
            var activeInstruction = await _instructionRepository.GetActiveByAsync(userId);

            if (activeInstruction is null) return null;

            activeInstruction.Cancel();

            await _instructionRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return activeInstruction;
        }

        public async Task<AggregatesModel.Instruction?> GetActiveByAsync(int userId)
        {
            return await _instructionRepository.GetActiveByAsync(userId);
        }

        public async Task<List<NotificationEnum>> GetNotificationsAsync(int userId)
        {
            var instruction = await _instructionRepository.GetActiveByAsync(userId);

            if (instruction == null) return new List<NotificationEnum>();

            return instruction.Notifications.Select(n => NotificationEnum.From(n.NotificationId)).ToList();
        }
    }
}
