﻿using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Entites.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace AC2C.Application.Identity
{

    public class ApplicationClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public static readonly string PhotoFileName = nameof(PhotoFileName);

        //private readonly IOptions<IdentityOptions> _optionsAccessor;
        //private readonly IApplicationRoleManager _roleManager;
        //private readonly IApplicationUserManager _userManager;

        public ApplicationClaimsPrincipalFactory(
            IApplicationUserManager userManager,
            IApplicationRoleManager roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base((UserManager<User>)userManager, (RoleManager<Role>)roleManager, optionsAccessor)
        {
            userManager.CheckArgumentIsNull(nameof(userManager));

           // _roleManager = roleManager;
            roleManager.CheckArgumentIsNull(nameof(roleManager));

            //_optionsAccessor = optionsAccessor;
            optionsAccessor.CheckArgumentIsNull(nameof(optionsAccessor));
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user).ConfigureAwait(false); // adds all `Options.ClaimsIdentity.RoleClaimType -> Role Claims` automatically + `Options.ClaimsIdentity.UserIdClaimType -> userId` & `Options.ClaimsIdentity.UserNameClaimType -> userName`
            AddCustomClaims(user, principal);
            return principal;
        }

        private static void AddCustomClaims(User user, IPrincipal principal)
        {
            ((ClaimsIdentity) principal.Identity).AddClaims(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer),
                new Claim(ClaimTypes.GivenName, user.FirstName ?? string.Empty),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(PhotoFileName, user.PhotoFileName ?? string.Empty, ClaimValueTypes.String),
            });
        }
    }
}