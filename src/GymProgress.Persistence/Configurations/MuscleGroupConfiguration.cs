using GymProgress.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymProgress.Persistence.Configurations
{
    public class MuscleGroupConfiguration : IEntityTypeConfiguration<MuscleGroup>
    {
        public void Configure(EntityTypeBuilder<MuscleGroup> builder)
        {
            builder.ToTable("MuscleGroups");
            builder.HasKey(mg => mg.Id);
            builder.Property(mg => mg.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new MuscleGroup { Id = 1, Name = "Chest" },
                new MuscleGroup { Id = 2, Name = "Back" },
                new MuscleGroup { Id = 3, Name = "Legs" },
                new MuscleGroup { Id = 4, Name = "Shoulders" },
                new MuscleGroup { Id = 5, Name = "Abs" },
                new MuscleGroup { Id = 6, Name = "Biceps" },
                new MuscleGroup { Id = 7, Name = "Triceps" },
                new MuscleGroup { Id = 8, Name = "Forearms" },
                new MuscleGroup { Id = 9, Name = "Core" }
            );
        }
    }
}
