using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Amplifier.AspNetCore.Authentication
{
    /// <summary>
    /// Provides the APIs to generate Amplifier default claims.
    /// </summary>
    public class ClaimManager : IClaimManager
    {
        /// <summary>
        /// Generate default Amplifier claims.
        /// </summary>
        /// <param name="userId">User unique identifier.</param>
        /// <param name="userName">User name.</param>
        /// <param name="tenantId">Tenant unique identifier.</param>
        /// <param name="userRoles">User roles list.</param>
        /// <returns></returns>
        public IList<Claim> GenerateDefaultClaims(string userId, string userName, string tenantId, IList<string> userRoles)
        {
            if(string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(tenantId))
            {
                throw new ArgumentNullException("Claim cannot be null");
            }
            var amplifierClaims = new List<Claim>()
            {
                new Claim("userid", userId, ClaimValueTypes.Integer),
                new Claim("username", userName, ClaimValueTypes.String),
                new Claim("tenantid", tenantId, ClaimValueTypes.Integer),
            };

            IEnumerable<Claim> rolesClaim = userRoles.Select(rn => new Claim("roles", rn, ClaimValueTypes.String));
            amplifierClaims.Add(new Claim("roles", rolesClaim.ToString(), ClaimValueTypes.String));

            return amplifierClaims;
        }        
    }
}
