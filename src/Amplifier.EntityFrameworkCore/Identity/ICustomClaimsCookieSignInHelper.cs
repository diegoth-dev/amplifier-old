using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amplifier.EntityFrameworkCore.Identity
{
    /// <summary>
    /// Interface for CustomClaimsCookieSignInHelper class
    /// </summary>
    /// <typeparam name="TIdentityUser">Application user class.</typeparam>
    /// <typeparam name="TKey">User primary key type.</typeparam>
    public interface ICustomClaimsCookieSignInHelper<TIdentityUser, TKey> 
        where TIdentityUser : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        /// <summary>
        /// Method for SignIn.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">Set whether the authentication session is persisted across multiple requests.</param>
        /// <param name="customClaims">Custom claims.</param>
        /// <returns></returns>
        Task SignInUserAsync(TIdentityUser user, bool isPersistent, IEnumerable<Claim> customClaims);
    }
}