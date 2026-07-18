// GymProgress.API/Extensions/ClaimsPrincipalExtensions.cs
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace GymProgress.API.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var value = user.FindFirstValue(JwtRegisteredClaimNames.Sub)
            ?? throw new UnauthorizedAccessException("User id claim missing from token.");

        return Guid.Parse(value);
    }
}