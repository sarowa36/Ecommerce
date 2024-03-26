using EntityLayer.Entities;
using IdentityLayer.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ToolsLayer.Encoder;
using ToolsLayer.ErrorModel;
using IdentityLayer;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly IMyUserManager<ApplicationUser> _userManager;
        readonly IHttpContextAccessor _httpContextAccessor;
        readonly IMySignInManager<ApplicationUser> _signInManager;
        readonly IServiceErrorContainer _serviceErrorContainer;
        HttpContext HttpContext { get { return _httpContextAccessor.HttpContext; } }
        public IdentityService(IMyUserManager<ApplicationUser> userManager,
            IMySignInManager<ApplicationUser> signInManager, 
            IHttpContextAccessor httpContextAccessor, 
            IServiceErrorContainer serviceErrorProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _serviceErrorContainer = serviceErrorProvider;
        }
        public async Task CreateUserAsync(ApplicationUser user, string password)
        {
            _serviceErrorContainer.BindError((await _userManager.CreateAsync(user, password)).ToErrorModel());
        }
        public async Task<string> EmailConfirmationTokenCreate(ApplicationUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }
        public async Task EmailConfirm(ApplicationUser user, string token)
        {
            _serviceErrorContainer.BindError( (await _userManager.ConfirmEmailAsync(user, token)).ToErrorModel());
        }
        #region Login
        public async Task LoginAsync(string usernameOrEmail, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
            {
                _serviceErrorContainer.AddError("ModelOnly", "User Not Found");
            }
            else
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);
                _serviceErrorContainer.BindError(result.ToErrorModel());
            }
        }
        public async Task LoginAsync(ApplicationUser user, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);
            _serviceErrorContainer.BindError(result.ToErrorModel());
        }
        public async Task LoginAsync(ApplicationUser user)
        {
            await _signInManager.SignInAsync(user, true);
        }
        #endregion
        public async Task<string> CreatePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        public async Task PasswordResetAsync(ApplicationUser user, string resetToken, string newPassword)
        {
            IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
            if (result.Succeeded)
                await _userManager.UpdateSecurityStampAsync(user);
            else
                _serviceErrorContainer.BindError(result.ToErrorModel());
        }
       
        public async Task<ApplicationUser> GetCurrentUserAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                _serviceErrorContainer.AddError("ModelOnly", "User Not Found");
            }
            return user;
        }
        public async Task<ApplicationUser> GetUser(string usernameOrEmail)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user == null)
                _serviceErrorContainer.AddError("ModelOnly", "User Not Found");
            return user;
        }
    }
}
