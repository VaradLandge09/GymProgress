using GymProgress.Application.DTOs.GymLogs;
using GymProgress.Application.Interfaces.Services;
using GymProgress.Domain.Entities;
using GymProgress.Domain.Interfaces.Repositories;
using System.Runtime.InteropServices;

namespace GymProgress.Application.Services
{
    public class GymLogService : IGymLogService
    {
        private readonly IGymLogRepository _gymLogRepository;
        private readonly IDayTypeRepository _dayTypeRepository;
        private readonly IMuscleGroupRepository _muscleGroupRepository;

        public GymLogService(IGymLogRepository gymLogRepository, IDayTypeRepository dayTypeRepository, IMuscleGroupRepository muscleGroupRepository)
        {
            _gymLogRepository = gymLogRepository;
            _dayTypeRepository = dayTypeRepository;
            _muscleGroupRepository = muscleGroupRepository;
        }

        public async Task<GymLogResponseDto> CreateGymLogAsync(Guid userId, GymLogPostRequestDto request, CancellationToken cancellationToken = default)
        {
            var dayType = await _dayTypeRepository.GetByNameAsync(request.DayType, cancellationToken)
                ?? throw new Exception(request.DayType + " Day type exception");

            var muscleGroups = await _muscleGroupRepository.GetByNameAsync(request.MuscleGroups, cancellationToken);

            var existing = await _gymLogRepository.GetByLogDateAndUserIdAsync(request.LogDate, userId, cancellationToken);

            if (existing is not null)
            {
                existing.UpdateType(dayType.Id, muscleGroups, request.Notes);
            }
            else
            {
                existing = new GymLog(userId, request.LogDate, dayType.Id, muscleGroups, request.Notes);
                await _gymLogRepository.AddGymLogAsync(existing, cancellationToken);
            }

            await _gymLogRepository.SaveChangesAsync(cancellationToken);

            var muscleGroupNames = muscleGroups.Select(mg => mg.Name).ToList();

            return new GymLogResponseDto(existing.Id, existing.LogDate, dayType.Name, muscleGroupNames, existing.Notes);
        }

        public async Task<List<GymLogResponseDto>> GetGymLogsByMonthAndYearAsync(Guid userId, GymLogGetRequestDto request, CancellationToken cancellationToken = default)
        {
            var gymLogs = await _gymLogRepository.GetAllByMonthAndYearAsync(userId, request.Month, request.Year, cancellationToken);

            return gymLogs
                .Select(gymLog => new GymLogResponseDto(
                        gymLog.Id,
                        gymLog.LogDate,
                        gymLog.DayType.Name,
                        gymLog.MuscleGroups.Select(mg => mg.Name).ToList(),
                        gymLog.Notes))
                    .ToList();
        }

        //public async Task<GymLogResponseDto> GetGymLogByDateAsync(GymLogGetRequestDto request, CancellationToken cancellationToken = default)
        //{
        //    var gymLog = await _gymLogRepository.GetByLogDateAndUserIdAsync(request.LogDate, request.UserId, cancellationToken);

        //    var response = new GymLogResponseDto(
        //        gymLog?.Id ?? 0,
        //        gymLog?.LogDate ?? DateOnly.MinValue,
        //        gymLog.DayType.Name ?? string.Empty,
        //        gymLog?.Notes ?? string.Empty
        //    );

        //    return response;
        //}
    }
}
