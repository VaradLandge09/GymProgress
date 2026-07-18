using GymProgress.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GymProgress.Persistence.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<GymLog> GymLog => Set<GymLog>();
    public DbSet<DayType> DayType => Set<DayType>();
    public DbSet<MuscleGroup> MuscleGroup => Set<MuscleGroup>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
