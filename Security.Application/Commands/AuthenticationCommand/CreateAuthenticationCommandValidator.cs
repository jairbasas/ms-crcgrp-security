using FluentValidation;

namespace Security.Application.Commands.AuthenticationCommand
{
    public class CreateAuthenticationCommandValidator : AbstractValidator<CreateAuthenticationCommand>
    {
        public CreateAuthenticationCommandValidator()
        {
            RuleFor(p => p.login)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");

            RuleFor(p => p.password)
                .NotEmpty().WithMessage("{PropertyName} no puede ser vacío");
        }
    }
}
