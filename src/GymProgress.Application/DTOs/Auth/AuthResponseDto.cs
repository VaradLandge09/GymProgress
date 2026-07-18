namespace GymProgress.Application.DTOs.Auth;

public record AuthResponseDto(
    Guid UserId,
    string FullName,
    string? userName,
    string Email,
    string AccessToken,
    DateTime AccessTokenExpiresAtUtc,
    string RefreshToken);
