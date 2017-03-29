using System.Threading.Tasks;
using AC2C.Application.Contracts.Identity;
using AC2C.Common.GuardToolkit;
using AC2C.Entites.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AC2C.Application.Identity
{
    /// <summary>
    /// More info: http://www.dotnettips.info/post/2578
    /// </summary>
    public class ApplicationSignInManager :
        SignInManager<User>,
        IApplicationSignInManager
    {
        //private readonly IApplicationUserManager _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        //private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;
        //private readonly IOptions<IdentityOptions> _optionsAccessor;
        //private readonly ILogger<ApplicationSignInManager> _logger;

        public ApplicationSignInManager(
            IApplicationUserManager userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<ApplicationSignInManager> logger)
            : base((UserManager<User>)userManager, contextAccessor, claimsFactory, optionsAccessor, logger)
        {
          //  _userManager = userManager;
            userManager.CheckArgumentIsNull(nameof(userManager));

            _contextAccessor = contextAccessor;
            _contextAccessor.CheckArgumentIsNull(nameof(_contextAccessor));

            //_claimsFactory = claimsFactory;
            claimsFactory.CheckArgumentIsNull(nameof(claimsFactory));

           // _optionsAccessor = optionsAccessor;
            optionsAccessor.CheckArgumentIsNull(nameof(optionsAccessor));

           // _logger = logger;
            logger.CheckArgumentIsNull(nameof(logger));
        }

        #region BaseClass

        #endregion

        #region CustomMethods

        public bool IsCurrentUserSignedIn()
        {
            return IsSignedIn(_contextAccessor.HttpContext.User);
        }

        public Task<User> ValidateCurrentUserSecurityStampAsync()
        {
            return ValidateSecurityStampAsync(_contextAccessor.HttpContext.User);
        }

        #endregion
    }
}
