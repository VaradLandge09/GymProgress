namespace GymProgress.Domain.Common;

/// <summary>
/// Common audit fields shared by every entity. Keeping this here means
/// every entity gets identity + timestamps for free, without repeating itself.
/// </summary>
public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
