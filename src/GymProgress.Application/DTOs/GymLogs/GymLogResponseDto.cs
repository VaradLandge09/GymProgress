using System.Security.Cryptography.X509Certificates;

namespace GymProgress.Application.DTOs.GymLogs
{
    public record GymLogResponseDto(
        int Id,
        DateOnly LogDate,
        string DayType,
        List<string> MuscleGroups,
        string? Notes
        );
}
