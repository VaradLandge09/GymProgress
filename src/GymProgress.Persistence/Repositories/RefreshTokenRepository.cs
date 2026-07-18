using GymProgress.Domain.Entities;
using GymProgress.Domain.Interfaces.Repositories;
using GymProgress.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GymProgress.Persistence.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RefreshTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<RefreshToken?> GetByTokenAsync(string token, CancellationToken cancellationToken = default)
        => _dbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);

    public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken = default)
        => await _dbContext.RefreshTokens.AddAsync(refreshToken, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
