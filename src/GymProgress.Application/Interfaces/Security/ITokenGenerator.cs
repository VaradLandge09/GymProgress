using GymProgress.Domain.Entities;

namespace GymProgress.Application.Interfaces.Security;

/// <summary>
/// Abstraction over token creation. Implemented by Infrastructure using JWT,
/// but AuthService only ever talks to this interface.
/// </summary>
public interface ITokenGenerator
{
    (string Token, DateTime ExpiresAtUtc) GenerateAccessToken(User user);
    string GenerateRefreshToken();
}
