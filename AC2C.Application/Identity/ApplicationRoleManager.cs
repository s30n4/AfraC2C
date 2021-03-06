﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Common.IdentityToolkit;
using AC2C.DataAccess.Context;
using AC2C.DataAccess.Uow.Contracts;
using AC2C.Dtos.Identity;
using AC2C.Entites.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AC2C.Application.Identity
{
  
    public class ApplicationRoleManager :
        RoleManager<Role>,
        IApplicationRoleManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DbSet<User> _users;

        public ApplicationRoleManager(
            IApplicationRoleStore store,
            IEnumerable<IRoleValidator<Role>> roleValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            ILogger<ApplicationRoleManager> logger,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork uow, IHttpContextAccessor contextAccessor1) :
            base((RoleStore<Role, ApplicationDbContext, int, UserRole, RoleClaim>)store, roleValidators, keyNormalizer, errors, logger, contextAccessor)
        {
            _contextAccessor = contextAccessor1;

            store.CheckArgumentIsNull(nameof(store));

            roleValidators.CheckArgumentIsNull(nameof(roleValidators));

            keyNormalizer.CheckArgumentIsNull(nameof(keyNormalizer));

            errors.CheckArgumentIsNull(nameof(errors));

            logger.CheckArgumentIsNull(nameof(logger));

            contextAccessor.CheckArgumentIsNull(nameof(contextAccessor));

            uow.CheckArgumentIsNull(nameof(uow));

            _users = uow.Set<User>();
        }

        #region BaseClass

        #endregion

        #region CustomMethods

        public IList<Role> FindCurrentUserRoles()
        {
            var userId = GetCurrentUserId();
            return FindUserRoles(userId);
        }

        public IList<Role> FindUserRoles(int userId)
        {
            var userRolesQuery = from role in Roles
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;

            return userRolesQuery.OrderBy(x => x.Name).ToList();
        }

        public Task<List<Role>> GetAllCustomRolesAsync()
        {
            return Roles.ToListAsync();
        }

        public IList<RoleAndUsersCountDto> GetAllCustomRolesAndUsersCountList()
        {
            return Roles.Select(role =>
                                    new RoleAndUsersCountDto
                                    {
                                        Role = role,
                                        UsersCount = role.Users.Count()
                                    }).ToList();
        }

        public async Task<PagedUsersListDto> GetPagedApplicationUsersInRoleListAsync(
                int roleId,
                int pageNumber, int recordsPerPage,
                string sortByField, SortOrder sortOrder,
                bool showAllUsers)
        {
            var skipRecords = pageNumber * recordsPerPage;

            var roleUserIdsQuery = from role in Roles
                                   where role.Id == roleId
                                   from user in role.Users
                                   select user.UserId;
            var query = _users.Include(user => user.Roles)
                              .Where(user => roleUserIdsQuery.Contains(user.Id))
                         .AsNoTracking();

            if (!showAllUsers)
            {
                query = query.Where(x => x.IsActive);
            }

            switch (sortByField)
            {
                case nameof(User.Id):
                    query = sortOrder == SortOrder.Descending ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                    break;
                default:
                    query = sortOrder == SortOrder.Descending ? query.OrderByDescending(x => x.Id) : query.OrderBy(x => x.Id);
                    break;
            }

            return new PagedUsersListDto
            {
                Paging =
                {
                    TotalItems = await query.CountAsync().ConfigureAwait(false)
                },
                Users = await query.Skip(skipRecords).Take(recordsPerPage).ToListAsync().ConfigureAwait(false),
                Roles = await Roles.ToListAsync().ConfigureAwait(false)
            };
        }


        public IList<User> GetApplicationUsersInRole(string roleName)
        {
            var roleUserIdsQuery = from role in Roles
                                   where role.Name == roleName
                                   from user in role.Users
                                   select user.UserId;
            return _users.Where(applicationUser => roleUserIdsQuery.Contains(applicationUser.Id))
                         .ToList();
        }

        public IList<Role> GetRolesForCurrentUser()
        {
            var userId = GetCurrentUserId();
            return GetRolesForUser(userId);
        }

        public IList<Role> GetRolesForUser(int userId)
        {
            var roles = FindUserRoles(userId);
            if (roles == null || !roles.Any())
            {
                return new List<Role>();
            }

            return roles.ToList();
        }

        public IList<UserRole> GetUserRolesInRole(string roleName)
        {
            return Roles.Where(role => role.Name == roleName)
                             .SelectMany(role => role.Users)
                             .ToList();
        }

        public bool IsCurrentUserInRole(string roleName)
        {
            var userId = GetCurrentUserId();
            return IsUserInRole(userId, roleName);
        }

        public bool IsUserInRole(int userId, string roleName)
        {
            var userRolesQuery = from role in Roles
                                 where role.Name == roleName
                                 from user in role.Users
                                 where user.UserId == userId
                                 select role;
            var userRole = userRolesQuery.FirstOrDefault();
            return userRole != null;
        }

        public Task<Role> FindRoleIncludeRoleClaimsAsync(int roleId)
        {
            return Roles.Include(x => x.Claims).FirstOrDefaultAsync(x => x.Id == roleId);
        }

        public async Task<IdentityResult> AddOrUpdateRoleClaimsAsync(
            int roleId,
            string roleClaimType,
            IList<string> selectedRoleClaimValues)
        {
            var role = await FindRoleIncludeRoleClaimsAsync(roleId).ConfigureAwait(false);
            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "RoleNotFound",
                    Description = "نقش مورد نظر یافت نشد."
                });
            }

            var currentRoleClaimValues = role.Claims.Where(roleClaim => roleClaim.ClaimType == roleClaimType)
                                                    .Select(roleClaim => roleClaim.ClaimValue)
                                                    .ToList();

            var newClaimValuesToAdd = selectedRoleClaimValues.Except(currentRoleClaimValues).ToList();
            foreach (var claimValue in newClaimValuesToAdd)
            {
                role.Claims.Add(new RoleClaim
                {
                    RoleId = role.Id,
                    ClaimType = roleClaimType,
                    ClaimValue = claimValue
                });
            }

            var removedClaimValues = currentRoleClaimValues.Except(selectedRoleClaimValues).ToList();
            foreach (var claimValue in removedClaimValues)
            {
                var roleClaim = role.Claims.SingleOrDefault(rc => rc.ClaimValue == claimValue &&
                                                                  rc.ClaimType == roleClaimType);
                if (roleClaim != null)
                {
                    role.Claims.Remove(roleClaim);
                }
            }

            return await UpdateAsync(role).ConfigureAwait(false);
        }

        private int GetCurrentUserId() => _contextAccessor.HttpContext.User.Identity.GetUserId<int>();

        #endregion
    }
}
