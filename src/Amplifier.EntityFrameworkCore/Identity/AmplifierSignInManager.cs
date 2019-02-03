using Amplifier.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Amplifier.EntityFrameworkCore.Identity
{
    /// <summary>
    /// Amplifier SignIn manager.
    /// </summary>
    /// <typeparam name="TIdentityUser">Application user class.</typeparam>
    /// <typeparam name="TKey">User class primary key type</typeparam>
    public class AmplifierSignInManager<TIdentityUser, TKey> : IAmplifierSignInManager<TIdentityUser, TKey> 
        where TIdentityUser : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        private readonly SignInManager<TIdentityUser> _signInManager;
        private readonly UserManager<TIdentityUser> _userManager;
        private readonly IClaimManager _claimManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpContext _context;

        /// <summary>
        /// Amplifier SignIn manager constructor.
        /// </summary>
        /// <param name="signInManager"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="claimManager"></param>
        /// <param name="userManager"></param>
        public AmplifierSignInManager(SignInManager<TIdentityUser> signInManager,
                                              IHttpContextAccessor contextAccessor,
                                              IClaimManager claimManager,
                                              UserManager<TIdentityUser> userManager)
        {
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
            _claimManager = claimManager;
            _userManager = userManager;
        }

        /// <summary>
        /// The <see cref="HttpContext"/> used.
        /// </summary>
        public HttpContext Context
        {
            get
            {
                var context = _context ?? _contextAccessor?.HttpContext;
                if (context == null)
                {
                    throw new InvalidOperationException("HttpContext must not be null.");
                }
                return context;
            }
            set
            {
                _context = value;
            }
        }

        /// <summary>
        /// Signs in the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="isPersistent">Set whether the authentication session is persisted across multiple requests.</param>
        /// <param name="customClaims">Custom claims.</param>
        /// <param name="tenantId">Tenant unique identifier.</param>
        /// <returns></returns>
        public async Task SignInUserAsync(TIdentityUser user, bool isPersistent, string tenantId,  IList<Claim> customClaims = null)
        {
            await SignInUserAsync(user, new AuthenticationProperties { IsPersistent = isPersistent }, tenantId, customClaims);
        }

        /// <summary>
        /// Signs in the specified <paramref name="user"/>.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="authenticationProperties"></param>
        /// <param name="customClaims"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public async Task SignInUserAsync(TIdentityUser user,
                                          AuthenticationProperties authenticationProperties,
                                          string tenantId,
                                          IList<Claim> customClaims = null)
        {
            IList<string> userRolesList = await _userManager.GetRolesAsync(user);
            var defaultClaims = _claimManager.GenerateDefaultClaims(user.Id.ToString(), user.UserName.ToString(),
                                                        tenantId, userRolesList);
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
            if (claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
            {
                claimsIdentity.AddClaims(defaultClaims);
                if(customClaims != null)
                    claimsIdentity.AddClaims(customClaims);
            }
            await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                claimsPrincipal, authenticationProperties);
        }

        /// <summary>
        /// Regenerates the user's application cookie, whilst preserving the existing
        /// AuthenticationProperties like rememberMe, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose sign-in cookie should be refreshed.</param>
        /// <param name="tenantId">The Tenant unique identifier.</param>
        /// <param name="customClaims">Custom claims list.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public async Task RefreshSignInAsync(TIdentityUser user, string tenantId, IList<Claim> customClaims = null)
        {
            IList<string> userRolesList = await _userManager.GetRolesAsync(user);
            var defaultClaims = _claimManager.GenerateDefaultClaims(user.Id.ToString(), user.UserName.ToString(),
                                                        tenantId, userRolesList);
            if(customClaims != null)
            {
                foreach (var claim in customClaims)
                {
                    defaultClaims.Add(claim);
                }
            }            

            var auth = await Context.AuthenticateAsync(IdentityConstants.ApplicationScheme);
            var authenticationMethod = auth?.Principal?.FindFirstValue(ClaimTypes.AuthenticationMethod);
            await SignInUserAsync(user, auth.Properties.IsPersistent, tenantId, defaultClaims);
        }
    }
}
