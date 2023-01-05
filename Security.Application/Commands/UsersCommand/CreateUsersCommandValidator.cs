using FluentValidation;
using FluentValidation.Validators;

namespace Security.Application.Commands.UsersCommand
{
    public class CreateUsersCommandValidator : AbstractValidator<CreateUsersCommand>
    {
        public CreateUsersCommandValidator()
        {
            RuleFor(p => p.userName)
                .NotEmpty().WithMessage("{PropertyName}, no debe estar vacío");

            RuleFor(p => p.email)
                .NotEmpty().WithMessage("{PropertyName}, no debe estar vacío")
                .EmailAddress().WithMessage("{PropertyName}, no es válido");

            RuleFor(p => p.state)
                .LessThan(3)
                .GreaterThan(0).WithMessage("{PropertyName}, tiene un valor incorrecto");
        }
    }
}
