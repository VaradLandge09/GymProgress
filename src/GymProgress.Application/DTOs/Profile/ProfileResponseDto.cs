using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Application.DTOs.Profile
{
    public record ProfileResponseDto(
        Guid UserId,
        string FullName,
        string? Username,
        string Email,
        DateTime MemberSince,
        int WeeklyGoalDays
     );
}
