using GymProgress.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Domain.Interfaces.Repositories
{
    public interface IDayTypeRepository
    {
        Task<DayType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
