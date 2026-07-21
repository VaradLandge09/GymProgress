// GymProgress.Application/Validators/GymLogPostRequestValidator.cs
using FluentValidation;
using GymProgress.Application.DTOs.GymLogs;

namespace GymProgress.Application.Validators;

public class GymLogPostRequestValidator : AbstractValidator<GymLogPostRequestDto>
{
    private static readonly string[] AllowedDayTypes = { "WeightTraining", "Cardio", "Rest" };

    public GymLogPostRequestValidator()
    {
        RuleFor(x => x.LogDate)
            .NotEmpty().WithMessage("Log date is required.")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow))
                .WithMessage("You can't log a workout for a future date.");

        RuleFor(x => x.DayType)
            .NotEmpty().WithMessage("Day type is required.")
            .Must(dt => AllowedDayTypes.Contains(dt))
                .WithMessage($"Day type must be one of: {string.Join(", ", AllowedDayTypes)}.");

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes can't exceed 500 characters.");
    }
}