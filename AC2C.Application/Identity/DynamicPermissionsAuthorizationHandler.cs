﻿using System;
using System.Threading.Tasks;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Common.WebToolkit;
using AC2C.Dtos.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AC2C.Application.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2581
    /// </summary>
    public class DynamicPermissionRequirement : IAuthorizationRequirement
    {
    }

    public class DynamicPermissionsAuthorizationHandler : AuthorizationHandler<DynamicPermissionRequirement>
    {
        private readonly ISecurityTrimmingService _securityTrimmingService;

        public DynamicPermissionsAuthorizationHandler(ISecurityTrimmingService securityTrimmingService)
        {
            _securityTrimmingService = securityTrimmingService;
            _securityTrimmingService.CheckArgumentIsNull(nameof(_securityTrimmingService));
        }

        protected override async Task HandleRequirementAsync(
             AuthorizationHandlerContext context,
             DynamicPermissionRequirement requirement)
        {
            var mvcContext = context.Resource as AuthorizationFilterContext;
            if (mvcContext == null)
            {
                return;
            }

            var actionDescriptor = mvcContext.ActionDescriptor;
            var area = actionDescriptor.RouteValues["area"];
            var controller = actionDescriptor.RouteValues["controller"];
            var action = actionDescriptor.RouteValues["action"];

            // How to access form values from an AuthorizationHandler
            var request = mvcContext.HttpContext.Request;
            if (request.Method.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                if (request.IsAjaxRequest() && request.ContentType.Contains("application/json"))
                {
                    var model = await request.DeserializeJsonBodyAsAsync<RoleDto>().ConfigureAwait(false);
                    if (model != null)
                    {

                    }
                }
                else
                {
                    foreach (var item in request.Form)
                    {
                        var formField = item.Key;
                        var formFieldValue = item.Value;
                    }
                }
            }

            if (_securityTrimmingService.CanCurrentUserAccess(area, controller, action))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}