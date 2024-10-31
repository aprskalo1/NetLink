using System.Security.Claims;

namespace NetLink.API.Utils;

public static class JwtClaimsHelper
{
    public static Guid GetDeveloperId(ClaimsPrincipal user)
    {
        var developerIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        if (developerIdClaim == null)
            throw new UnauthorizedAccessException("Developer ID not found in token.");

        return Guid.Parse(developerIdClaim.Value);
    }
}