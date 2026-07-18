using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Application.DTOs.GymLogs
{
    public record GymLogPostRequestDto(
        DateOnly LogDate,
        string DayType,
        List<string> MuscleGroups,
        string? Notes
        );
}
