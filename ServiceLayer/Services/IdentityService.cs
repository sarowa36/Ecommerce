using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;
using Microsoft.AspNetCore.Identity;
using ServiceLayer.Base.Services;
using ServiceLayer.ServiceResults.Identity;
using ToolsLayer.Encoder;
using ToolsLayer.ErrorModel;

namespace ServiceLayer.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        readonly SignInManager<ApplicationUser> _signInManager;
        readonly IMailService _mailService;

        public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IMailService mailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
        }
        public async Task<CreateUserResponse> CreateUserAsync(RegisterVM model)
        {
            var response=new CreateUserResponse();
            response.Value = new()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };
            response.Errors = (await _userManager.CreateAsync(response.Value, model.Password)).ToErrorModel();
            if (response.IsSuccess)
            {
                await _userManager.AddToRoleAsync(response.Value, "User");
            }
            return response;
        }

        public async Task<AssignRolesToUserResponse> AssignRolesToUserAsnyc(string userId, string[] roles)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var response = new AssignRolesToUserResponse();
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);
            }
            else
                response.Errors.Add("ModelOnly", "User Not Found");
            return response;
        }
        public async Task<GetUserRolesResponse> GetUserRolesAsync(string userIdOrName)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userIdOrName);
            var response = new GetUserRolesResponse();
            if (user == null)
                user = await _userManager.FindByNameAsync(userIdOrName);

            if (user != null)
            {
                response.Value = (await _userManager.GetRolesAsync(user)).ToArray();
            }
            else
                response.Errors.Add("ModelOnly","User Not Found");
            return response;
        }
        public async Task<LoginResponse> LoginAsync(string usernameOrEmail, string password)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(usernameOrEmail);
            var response= new LoginResponse();
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);
            if (user != null)
            {
                SignInResult result = await _signInManager.PasswordSignInAsync(user, password, true, false);
                response.Value = user;
                response.Errors = result.ToErrorModel();
            }
            else
                response.Errors.Add("ModelOnly", "User Not Found");
            return response;
        }
        public async Task<UpdatePasswordResponse> UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            var response = new UpdatePasswordResponse();
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();
                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    response.Errors = result.ToErrorModel();
            }
            else
                response.Errors.Add("ModelOnly", "User Not Found");
            return response;
        }
        public async Task<CreatePasswordResetResponse> CreatePasswordResetRequestAsync(string email)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var response = new CreatePasswordResetResponse();
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
            else
                response.Errors.Add("ModelOnly", "User Not Found");
            return response;
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }

    }
}
