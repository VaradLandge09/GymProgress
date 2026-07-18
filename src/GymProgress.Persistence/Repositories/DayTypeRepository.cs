using GymProgress.Domain.Entities;
using GymProgress.Domain.Interfaces.Repositories;
using GymProgress.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymProgress.Persistence.Repositories
{
    public class DayTypeRepository : IDayTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DayTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<DayType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => _dbContext.DayType.FirstOrDefaultAsync(dt => dt.Name == name, cancellationToken);
    }
}
