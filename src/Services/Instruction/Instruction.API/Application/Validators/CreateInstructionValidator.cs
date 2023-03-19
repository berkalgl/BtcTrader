using FluentValidation;
using Instruction.API.Application.DTOs;

namespace Instruction.API.Application.Validators
{
    public class CreateInstructionValidator : AbstractValidator<CreateInstructionDTO>
    {
        public CreateInstructionValidator()
        {

            RuleFor(x => x.TransactionDay)
                .InclusiveBetween(1, 28)
                .WithMessage("Day must be between 1 and 28");

            RuleFor(x => x.Amount)
                .InclusiveBetween(100, 20000)
                .WithMessage("Amount must be in range 100 and 20.000");

            RuleFor(x => x.Notifications)
                .Must(x => x.Count > 0)
                .WithMessage("Notification preference must be selected")
                .ForEach(notification =>
                {
                    notification
                    .IsInEnum()
                    .WithMessage("Notification must be defined");
                });        
        }
    }
}
