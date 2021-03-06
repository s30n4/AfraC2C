﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Common.IdentityToolkit;
using AC2C.Common.WebToolkit;
using AC2C.DataAccess.Context;
using AC2C.DataAccess.Uow.Contracts;
using AC2C.Dtos.Identity;
using AC2C.Entites.Identity;
using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AC2C.Application.Identity
{
   
    public class ApplicationUserManager :
        UserManager<User>,
        IApplicationUserManager
    {
        private readonly IHttpContextAccessor _contextAccessor;
      //  private readonly IUnitOfWork _uow;
        private readonly IUsedPasswordsService _usedPasswordsService;
        //private readonly IdentityErrorDescriber _errors;
        //private readonly ILookupNormalizer _keyNormalizer;
        //private readonly ILogger<ApplicationUserManager> _logger;
       // private readonly IOptions<IdentityOptions> _optionsAccessor;
       // private readonly IPasswordHasher<User> _passwordHasher;
       // private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
       // private readonly IServiceProvider _services;
        private readonly DbSet<User> _users;
        private readonly DbSet<Role> _roles;
       // private readonly IApplicationUserStore _userStore;
      //  private readonly IEnumerable<IUserValidator<User>> _userValidators;
        private User _currentUserInScope;

        public ApplicationUserManager(
            IApplicationUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<ApplicationUserManager> logger,
            IHttpContextAccessor contextAccessor,
            IUnitOfWork uow,
            IUsedPasswordsService usedPasswordsService)
            : base((UserStore<User, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>)store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
          //  _userStore = store;
            store.CheckArgumentIsNull(nameof(store));

          //  _optionsAccessor = optionsAccessor;
            optionsAccessor.CheckArgumentIsNull(nameof(optionsAccessor));

           // _passwordHasher = passwordHasher;
            passwordHasher.CheckArgumentIsNull(nameof(passwordHasher));

          //  _userValidators = userValidators;
            userValidators.CheckArgumentIsNull(nameof(userValidators));

           // _passwordValidators = passwordValidators;
            passwordValidators.CheckArgumentIsNull(nameof(passwordValidators));

            //_keyNormalizer = keyNormalizer;
            keyNormalizer.CheckArgumentIsNull(nameof(keyNormalizer));

            //_errors = errors;
            errors.CheckArgumentIsNull(nameof(errors));

           // _services = services;
            services.CheckArgumentIsNull(nameof(services));

            //_logger = logger;
            logger.CheckArgumentIsNull(nameof(logger));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

           // _uow = uow;
            uow.CheckArgumentIsNull(nameof(uow));

            _usedPasswordsService = usedPasswordsService;
            _usedPasswordsService.CheckArgumentIsNull(nameof(_usedPasswordsService));

            _users = uow.Set<User>();
            _roles = uow.Set<Role>();
        }

        #region BaseClass

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            var result = await base.CreateAsync(user).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user).ConfigureAwait(false);
            }
            return result;
        }

        public override async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await base.CreateAsync(user, password).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user).ConfigureAwait(false);
            }
            return result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(User user, string currentPassword, string newPassword)
        {
            var result = await base.ChangePasswordAsync(user, currentPassword, newPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user).ConfigureAwait(false);
            }
            return result;
        }

        public override async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            var result = await base.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
            if (result.Succeeded)
            {
                await _usedPasswordsService.AddToUsedPasswordsListAsync(user).ConfigureAwait(false);
            }
            return result;
        }

        #endregion

        #region CustomMethods

        public User FindById(int userId)
        {
            return _users.Find(userId);
        }

        public Task<User> FindByIdIncludeUserRolesAsync(int userId)
        {
            return _users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            return Users.ToListAsync();
        }

        public User GetCurrentUser()
        {
            if (_currentUserInScope != null)
            {
                return _currentUserInScope;
            }

            var currentUserId = GetCurrentUserId();
            if (string.IsNullOrWhiteSpace(currentUserId))
            {
                return null;
            }

            var userId = int.Parse(currentUserId);
            return _currentUserInScope = FindById(userId);
        }

        public async Task<User> GetCurrentUserAsync()
        {
            return _currentUserInScope ??
                (_currentUserInScope = await GetUserAsync(_contextAccessor.HttpContext.User).ConfigureAwait(false));
        }

        public string GetCurrentUserId()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserId();
        }

        public int? CurrentUserId
        {
            get
            {
                var userId = _contextAccessor.HttpContext.User.Identity.GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return null;
                }

                int result;
                return !int.TryParse(userId, out result) ? (int?)null : result;
            }
        }

        public string GetCurrentUserName()
        {
            return _contextAccessor.HttpContext.User.Identity.GetUserName();
        }

        public async Task<bool> HasPasswordAsync(int userId)
        {
            var user = await FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            return user?.PasswordHash != null;
        }

        public async Task<bool> HasPhoneNumberAsync(int userId)
        {
            var user = await FindByIdAsync(userId.ToString()).ConfigureAwait(false);
            return user?.PhoneNumber != null;
        }

        public async Task<byte[]> GetEmailImageAsync(int? userId)
        {
            if (userId == null)
                return TextToImage.EMailToImage("?");

            var user = await FindByIdAsync(userId.Value.ToString()).ConfigureAwait(false);
            if (user == null)
                return TextToImage.EMailToImage("?");

            if (!user.IsEmailPublic)
                return TextToImage.EMailToImage("?");

            return TextToImage.EMailToImage(user.Email);
        }

        public async Task<PagedUsersListDto> GetPagedUsersListAsync(SearchUsersDto model, int pageNumber)
        {
            var skipRecords = pageNumber * model.MaxNumberOfRows;
            var query = _users.Include(x => x.Roles).AsNoTracking();

            if (!model.ShowAllUsers)
            {
                query = query.Where(x => x.IsActive == model.UserIsActive);
            }

            if (!string.IsNullOrWhiteSpace(model.TextToFind))
            {
                model.TextToFind = model.TextToFind.ApplyCorrectYeKe();

                if (model.IsPartOfEmail)
                {
                    query = query.Where(x => x.Email.Contains(model.TextToFind));
                }

                if (model.IsUserId)
                {
                    int userId;
                    if (int.TryParse(model.TextToFind, out userId))
                    {
                        query = query.Where(x => x.Id == userId);
                    }
                }

                if (model.IsPartOfName)
                {
                    query = query.Where(x => x.FirstName.Contains(model.TextToFind));
                }

                if (model.IsPartOfLastName)
                {
                    query = query.Where(x => x.LastName.Contains(model.TextToFind));
                }

                if (model.IsPartOfUserName)
                {
                    query = query.Where(x => x.UserName.Contains(model.TextToFind));
                }

                if (model.IsPartOfLocation)
                {
                    query = query.Where(x => x.Location.Contains(model.TextToFind));
                }
            }

            if (model.HasEmailConfirmed)
            {
                query = query.Where(x => x.EmailConfirmed);
            }

            if (model.UserIsLockedOut)
            {
                query = query.Where(x => x.LockoutEnd != null);
            }

            if (model.HasTwoFactorEnabled)
            {
                query = query.Where(x => x.TwoFactorEnabled);
            }

            query = query.OrderBy(x => x.Id);
            return new PagedUsersListDto
            {
                Paging =
                {
                    TotalItems = await query.CountAsync().ConfigureAwait(false)
                },
                Users = await query.Skip(skipRecords).Take(model.MaxNumberOfRows).ToListAsync().ConfigureAwait(false),
                Roles = await _roles.ToListAsync().ConfigureAwait(false)
            };
        }

        public async Task<PagedUsersListDto> GetPagedUsersListAsync(
            int pageNumber, int recordsPerPage,
            string sortByField, SortOrder sortOrder,
            bool showAllUsers)
        {
            var skipRecords = pageNumber * recordsPerPage;
            var query = _users.Include(x => x.Roles).AsNoTracking();

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
                Roles = await _roles.ToListAsync().ConfigureAwait(false)
            };
        }

        public async Task<IdentityResult> UpdateUserAndSecurityStampAsync(int userId, Action<User> action)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            action(user);

            var result = await UpdateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user).ConfigureAwait(false);
        }

        public async Task<IdentityResult> AddOrUpdateUserRolesAsync(int userId, IList<int> selectedRoleIds, Action<User> action = null)
        {
            var user = await FindByIdIncludeUserRolesAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = "کاربر مورد نظر یافت نشد."
                });
            }

            var currentUserRoleIds = user.Roles.Select(x => x.RoleId).ToList();

            var newRolesToAdd = selectedRoleIds.Except(currentUserRoleIds).ToList();
            foreach (var roleId in newRolesToAdd)
            {
                user.Roles.Add(new UserRole { RoleId = roleId, UserId = user.Id });
            }

            var removedRoles = currentUserRoleIds.Except(selectedRoleIds).ToList();
            foreach (var roleId in removedRoles)
            {
                var userRole = user.Roles.SingleOrDefault(ur => ur.RoleId == roleId);
                if (userRole != null)
                {
                    user.Roles.Remove(userRole);
                }
            }

            action?.Invoke(user);

            var result = await UpdateAsync(user).ConfigureAwait(false);
            if (!result.Succeeded)
            {
                return result;
            }
            return await UpdateSecurityStampAsync(user).ConfigureAwait(false);
        }

        #endregion
    }
}