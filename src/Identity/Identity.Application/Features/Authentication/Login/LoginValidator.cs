using FluentValidation;

namespace Identity.Application.Features.Authentication.Login;

public class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName can't be null or empty")
            .MinimumLength(3).WithMessage("UserName's lenght must be longer than 3 chars");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password can't be null or empty")
            .MinimumLength(3).WithMessage("Password's lenght must be longer than 3 chars");
    }
}
