using GymProgress.Domain.Common;

namespace GymProgress.Domain.Entities;

    public class GymLog
    {

    // Used only by EF Core to materialize rows from the database.
    // Navigation properties (MuscleGroups, DayType, User) get populated
    // separately via EF's fixup after this runs — never pass them here.
    private GymLog() { }


    public GymLog(Guid userId, DateOnly logDate, int dayTypeId, List<MuscleGroup> muscleGroups, string? notes)
    {
        UserId = userId;
        LogDate = logDate;
        DayTypeId = dayTypeId;
        Notes = notes;
        MuscleGroups = muscleGroups;
        CreatedAt = DateTime.UtcNow;
    }

    public int Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public DateOnly LogDate { get; set; }

        public int DayTypeId { get; set; }
        public DayType DayType { get; set; } = null!;

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<MuscleGroup> MuscleGroups { get; set; } = new List<MuscleGroup>();

    public void UpdateType(int dayTypeId, List<MuscleGroup> muscleGroups, string? notes)
    {
        DayTypeId = dayTypeId;
        Notes = notes;
        MuscleGroups = muscleGroups;
        UpdatedAt = DateTime.UtcNow;
    }
}

