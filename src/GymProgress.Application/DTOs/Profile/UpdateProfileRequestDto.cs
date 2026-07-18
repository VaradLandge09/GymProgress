using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Application.DTOs.Profile
{
    public record UpdateProfileRequestDto(string? FullName, string? Email, string? Username, int? WeeklyGoalDays);
}
