using GymProgress.Domain.Common;

namespace GymProgress.Domain.Entities;

/// <summary>
/// A long-lived token that lets the Flutter app silently obtain a new access
/// token without forcing the user to log in again every time the JWT expires.
/// </summary>
public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAtUtc { get; set; }
    public bool IsRevoked { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAtUtc;
    public bool IsActive => !IsRevoked && !IsExpired;
}
