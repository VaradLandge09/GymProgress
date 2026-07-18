using GymProgress.Domain.Entities;

namespace GymProgress.Domain.Interfaces.Repositories;

/// <summary>
/// Contract owned by the Domain layer and implemented by Persistence.
/// The Application layer depends only on this abstraction (Dependency
/// Inversion Principle) so it never needs to know EF Core or SQL Server exist.
/// </summary>
public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task AddAsync(User user, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
