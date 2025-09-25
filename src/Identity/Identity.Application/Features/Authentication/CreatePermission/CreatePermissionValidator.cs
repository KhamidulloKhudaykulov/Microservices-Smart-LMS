using FluentValidation;
using FluentValidation.Validators;

namespace Identity.Application.Features.Authentication.CreatePermission;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionCommand>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.PermissionName)
            .MinimumLength(3).WithMessage("Permission name must be longer than 3");
    }
}
