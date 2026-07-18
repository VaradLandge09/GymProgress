using GymProgress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Domain.Interfaces.Repositories
{
    public interface IGymLogRepository
    {
        Task<GymLog?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<GymLog?> GetByLogDateAndUserIdAsync(DateOnly logDate, Guid userId, CancellationToken cancellationToken = default);
        Task<List<GymLog>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<List<GymLog>> GetAllByMonthAndYearAsync(Guid userId, int? month, int? year, CancellationToken cancellationToken = default);
        Task AddGymLogAsync(GymLog gymLog, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
