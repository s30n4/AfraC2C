﻿using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Common.WebToolkit;
using Microsoft.AspNetCore.Http;

namespace AC2C.Application.Identity
{
    
    public class SecurityTrimmingService : ISecurityTrimmingService
    {
        private readonly HttpContext _httpContext;
      //  private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMvcActionsDiscoveryService _mvcActionsDiscoveryService;

        public SecurityTrimmingService(
            IHttpContextAccessor httpContextAccessor,
            IMvcActionsDiscoveryService mvcActionsDiscoveryService)
        {
          //  _httpContextAccessor = httpContextAccessor;
            httpContextAccessor.CheckArgumentIsNull(nameof(httpContextAccessor));

            _httpContext = httpContextAccessor.HttpContext;

            _mvcActionsDiscoveryService = mvcActionsDiscoveryService;
            _mvcActionsDiscoveryService.CheckArgumentIsNull(nameof(_mvcActionsDiscoveryService));
        }

        public bool CanCurrentUserAccess(string area, string controller, string action)
        {
            return _httpContext != null && CanUserAccess(_httpContext.User, area, controller, action);
        }

        public bool CanUserAccess(ClaimsPrincipal user, string area, string controller, string action)
        {
            var currentClaimValue = $"{area}:{controller}:{action}";
            var securedControllerActions = _mvcActionsDiscoveryService.GetAllSecuredControllerActionsWithPolicy(ConstantPolicies.DynamicPermission);
            if (securedControllerActions.SelectMany(x => x.MvcActions).All(x => x.ActionId != currentClaimValue))
            {
                throw new KeyNotFoundException($@"The `secured` area={area}/controller={controller}/action={action} with `ConstantPolicies.DynamicPermission` policy not found. Please check you have entered the area/controller/action names correctly and also it's decorated with the correct security policy.");
            }

            if (!user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (user.IsInRole(ConstantRoles.Admin))
            {
                // Admin users have access to all of the pages.
                return true;
            }

            // Check for dynamic permissions
            // A user gets its permissions claims from the `ApplicationClaimsPrincipalFactory` class automatically and it includes the role claims too.
            return user.HasClaim(claim => claim.Type == ConstantPolicies.DynamicPermissionClaimType &&
                                          claim.Value == currentClaimValue);
        }
    }
}