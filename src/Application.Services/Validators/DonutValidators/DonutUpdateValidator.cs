using Application.Services.Commands.DonutCommands;
using FluentValidation;

namespace Application.Services.Validators.DonutValidators
{
    public class DonutUpdateValidator : AbstractValidator<DonutUpdateCommand>
    {
        public DonutUpdateValidator() 
        {
            RuleFor(x=>x.Name).NotEmpty();
        }
    }
}
