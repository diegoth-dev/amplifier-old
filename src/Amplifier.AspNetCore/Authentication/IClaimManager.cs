using System.Collections.Generic;
using System.Security.Claims;

namespace Amplifier.AspNetCore.Authentication
{
    public interface IClaimManager
    {
        /// <summary>
        /// Generate default Amplifier claims.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="userName">User name.</param>
        /// <param name="tenantId">Tenant unique identifier.</param>
        /// <param name="userRoles">User roles list.</param>
        /// <returns></returns>
        IList<Claim> GenerateDefaultClaims(string userId, string userName, string tenantId, IList<string> userRoles);
    }
}