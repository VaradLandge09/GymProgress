using GymProgress.Domain.Entities;
using GymProgress.Domain.Interfaces.Repositories;
using GymProgress.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymProgress.Persistence.Repositories
{
    public class GymLogRepository : IGymLogRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GymLogRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<GymLog?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => _dbContext.GymLog.FirstOrDefaultAsync(u => u.UserId == userId, cancellationToken);

        public Task<GymLog?> GetByLogDateAndUserIdAsync(DateOnly logDate, Guid userId, CancellationToken cancellationToken = default)
            => _dbContext.GymLog.FirstOrDefaultAsync(u => u.LogDate == logDate && u.UserId == userId, cancellationToken);

        public Task<List<GymLog>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
            => _dbContext.GymLog
            .Where(u => u.UserId == userId)
            .OrderByDescending(u => u.LogDate)
            .ToListAsync(cancellationToken);

        public async Task AddGymLogAsync(GymLog gymLog, CancellationToken cancellationToken = default)
            => await _dbContext.GymLog.AddAsync(gymLog, cancellationToken);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => _dbContext.SaveChangesAsync(cancellationToken);

        public Task<List<GymLog>> GetAllByMonthAndYearAsync(Guid userId, int? month, int? year, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.GymLog
                .Include(g => g.DayType)
                .Include(mg => mg.MuscleGroups)
                .Where(u => u.UserId == userId);

            if(month is not null && year is not null)
            {
                query = query.Where(g => g.LogDate.Month == month && g.LogDate.Year == year);
            }

            return 
                query.OrderByDescending(g => g.LogDate)
                .ToListAsync(cancellationToken);
        }
            
    }
}
