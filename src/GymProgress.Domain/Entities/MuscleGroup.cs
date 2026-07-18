
namespace GymProgress.Domain.Entities
{
    public class MuscleGroup
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ICollection<GymLog> GymLogs { get; set; } = new List<GymLog>();
    }
}
