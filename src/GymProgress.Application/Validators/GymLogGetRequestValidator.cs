// GymProgress.Application/Validators/GymLogGetRequestValidator.cs
using FluentValidation;
using GymProgress.Application.DTOs.GymLogs;

namespace GymProgress.Application.Validators;

public class GymLogGetRequestValidator : AbstractValidator<GymLogGetRequestDto>
{
    public GymLogGetRequestValidator()
    {
        RuleFor(x => x.Month)
            .InclusiveBetween(1, 12).When(x => x.Month is not null)
            .WithMessage("Month must be between 1 and 12.");

        RuleFor(x => x.Year)
            .GreaterThan(2000).When(x => x.Year is not null)
            .WithMessage("Year must be valid.");

        // Both or neither — don't allow month without year or vice versa
        RuleFor(x => x)
            .Must(x => (x.Month is null) == (x.Year is null))
            .WithMessage("Month and Year must be provided together.");
    }
}