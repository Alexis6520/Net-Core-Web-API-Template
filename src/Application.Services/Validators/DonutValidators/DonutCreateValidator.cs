using Application.Services.Commands.DonutCommands;
using FluentValidation;

namespace Application.Services.Validators.DonutValidators
{
    public class DonutCreateValidator : AbstractValidator<DonutCreateCommand>
    {
        public DonutCreateValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
