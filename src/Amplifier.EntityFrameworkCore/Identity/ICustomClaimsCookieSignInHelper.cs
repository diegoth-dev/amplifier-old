using Microsoft.AspNetCore.Identity;
using System;

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
    }
}