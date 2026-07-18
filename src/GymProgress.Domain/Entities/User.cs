using GymProgress.Domain.Common;

namespace GymProgress.Domain.Entities;

/// <summary>
/// A registered Gym Progress user. Only holds identity and credential data —
/// workout logs (Phase 1's core feature) will live in their own entity later,
/// keeping this class focused on a single responsibility: authentication identity.
/// </summary>
public class User : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Username { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public int WeeklyGoalDays { get; set; } = 4;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
