using GymProgress.Application.DTOs.GymLogs;

namespace GymProgress.Application.Interfaces.Services
{
    public interface IGymLogService
    {
        //Task<GymLogResponseDto> GetGymLogByDateAsync(GymLogGetRequestDto request, CancellationToken cancellationToken = default);
        Task<List<GymLogResponseDto>> GetGymLogsByMonthAndYearAsync(Guid userId, GymLogGetRequestDto request ,CancellationToken cancellationToken = default);
        Task<GymLogResponseDto> CreateGymLogAsync(Guid userId, GymLogPostRequestDto request, CancellationToken cancellationToken = default);
    }
}
