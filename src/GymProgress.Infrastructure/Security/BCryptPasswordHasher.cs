using GymProgress.Application.Interfaces.Security;

namespace GymProgress.Infrastructure.Security;

/// <summary>
/// BCrypt implementation of IPasswordHasher. Nothing above this layer
/// knows or cares that BCrypt is the algorithm in use.
/// </summary>
public class BCryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 12;

    public string Hash(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);

    public bool Verify(string password, string passwordHash)
        => BCrypt.Net.BCrypt.Verify(password, passwordHash);
}
