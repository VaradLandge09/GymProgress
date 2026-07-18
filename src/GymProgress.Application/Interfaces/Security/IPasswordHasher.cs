namespace GymProgress.Application.Interfaces.Security;

/// <summary>
/// Abstraction over the hashing algorithm. AuthService depends on this,
/// not on BCrypt directly, so the hashing strategy can change (Open/Closed
/// Principle) without touching a single line of business logic.
/// </summary>
public interface IPasswordHasher
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}
