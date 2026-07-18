namespace GymProgress.Infrastructure.Security;

/// <summary>
/// Strongly-typed mirror of the "JwtSettings" section in appsettings.json.
/// Binding to a class instead of reading raw strings means typos in config
/// keys fail fast instead of causing silent bugs.
/// </summary>
public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenExpirationMinutes { get; set; } = 60;
}
