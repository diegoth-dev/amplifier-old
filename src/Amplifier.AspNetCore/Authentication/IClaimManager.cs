using System.Collections.Generic;
using System.Security.Claims;

namespace Amplifier.AspNetCore.Authentication
{
    public interface IClaimManager
    {
        Claim[] GenerateDefaultClaims(string userId, string userName, string tenantId, IList<string> userRoles);
    }
}