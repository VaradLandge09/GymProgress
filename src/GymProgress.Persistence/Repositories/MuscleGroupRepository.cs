using GymProgress.Domain.Entities;
using GymProgress.Domain.Interfaces.Repositories;
using GymProgress.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Persistence.Repositories
{
    public class MuscleGroupRepository : IMuscleGroupRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MuscleGroupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<MuscleGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
            => _dbContext.MuscleGroup.FirstOrDefaultAsync(mg => mg.Name == name, cancellationToken);

        public Task<List<MuscleGroup>> GetByNameAsync(List<string> names, CancellationToken cancellationToken = default)
            => _dbContext.MuscleGroup.Where(mg => names.Contains(mg.Name)).ToListAsync(cancellationToken);
    }
}
