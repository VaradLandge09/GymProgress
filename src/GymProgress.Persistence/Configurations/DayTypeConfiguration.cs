using GymProgress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymProgress.Persistence.Configurations;

    public class DayTypeConfiguration : IEntityTypeConfiguration<DayType>
    {
        public void Configure(EntityTypeBuilder<DayType> builder)
    {
        builder.ToTable("DayType");
        builder.HasKey(dt => dt.Id);
        builder.Property(dt => dt.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasData(
            new DayType { Id = 1, Name = "WeightTraining" },
            new DayType { Id = 2, Name = "Cardio" },
            new DayType { Id = 3, Name = "Rest" }
        );
    }
}

