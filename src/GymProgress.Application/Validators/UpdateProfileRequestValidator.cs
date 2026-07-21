// GymProgress.Application/Validators/UpdateProfileRequestValidator.cs
using FluentValidation;
using GymProgress.Application.DTOs.Profile;

public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequestDto>
{
    public UpdateProfileRequestValidator()
    {
        RuleFor(x => x.Username)
            .MaximumLength(50).WithMessage("Username can't exceed 50 characters.")
            .When(x => x.Username is not null);
    }
}