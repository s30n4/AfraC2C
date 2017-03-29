using System.Security.Claims;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.DataAccess.Context;
using AC2C.DataAccess.Uow.Contracts;
using AC2C.Entites.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AC2C.Application.Identity
{
   
    public class ApplicationUserStore :
        UserStore<User, Role, ApplicationDbContext, int, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>,
        IApplicationUserStore
    {
      //  private readonly IUnitOfWork _uow;
     //   private readonly IdentityErrorDescriber _describer;

        public ApplicationUserStore(
            IUnitOfWork uow,
            IdentityErrorDescriber describer)
            : base((ApplicationDbContext)uow, describer)
        {
           // _uow = uow;
            uow.CheckArgumentIsNull(nameof(uow));

           // _describer = describer;
            describer.CheckArgumentIsNull(nameof(describer));
        }


        #region BaseClass

        protected override UserClaim CreateUserClaim(User user, Claim claim)
        {
            var userClaim = new UserClaim { UserId = user.Id };
            userClaim.InitializeFromClaim(claim);
            return userClaim;
        }

        protected override UserLogin CreateUserLogin(User user, UserLoginInfo login)
        {
            return new UserLogin
            {
                UserId = user.Id,
                ProviderKey = login.ProviderKey,
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName
            };
        }

        protected override UserRole CreateUserRole(User user, Role role)
        {
            return new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
        }

        protected override UserToken CreateUserToken(User user, string loginProvider, string name, string value)
        {
            return new UserToken
            {
                UserId = user.Id,
                LoginProvider = loginProvider,
                Name = name,
                Value = value
            };
        }

        #endregion

        #region CustomMethods

        // Add custom methods here

        #endregion
    }
}