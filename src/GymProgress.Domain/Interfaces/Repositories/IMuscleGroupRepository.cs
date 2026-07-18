using GymProgress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Domain.Interfaces.Repositories
{
    public interface IMuscleGroupRepository
    {
        Task<MuscleGroup?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<List<MuscleGroup>> GetByNameAsync(List<string> names, CancellationToken cancellationToken = default);

    }
}
