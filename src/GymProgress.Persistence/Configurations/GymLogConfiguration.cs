using GymProgress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymProgress.Persistence.Configurations
{
    public class GymLogConfiguration : IEntityTypeConfiguration<GymLog>
    {
        public void Configure(EntityTypeBuilder<GymLog> builder)
        {
            builder.ToTable("GymLog");

            builder.HasKey(gl => gl.Id);

            builder.Property(gl => gl.Id)
                .ValueGeneratedOnAdd();

            builder.Property(gl => gl.UserId)
                .IsRequired();

            builder.Property(gl => gl.LogDate)
                .IsRequired();

            builder.Property(gl => gl.DayTypeId)
                .IsRequired();

            builder.Property(gl => gl.Notes)
                .HasMaxLength(500);

            builder.Property(gl => gl.CreatedAt)
                .IsRequired();

            // Foreign key -> Users
            builder.HasOne(gl => gl.User)
                .WithMany()
                .HasForeignKey(gl => gl.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Foreign key -> DayTypes
            builder.HasOne(gl => gl.DayType)
                .WithMany()
                .HasForeignKey(gl => gl.DayTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-many relationship between GymLog and MuscleGroup
            builder.HasMany(g => g.MuscleGroups)
                .WithMany(m => m.GymLogs)
                .UsingEntity(j => j.ToTable("GymLogMuscleGroups"));

            // Unique constraint to ensure a user can only have one log per day
            builder.HasIndex(gl => new { gl.UserId, gl.LogDate })
                .IsUnique();
        }
    }
}
