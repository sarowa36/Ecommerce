using AutoMapper;
using BusinessLayer.Validations.IdentityController;
using Ecommerce.AutoMappers.IdentityController;
using Ecommerce.Helpers;
using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{

    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public IdentityController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var validationResult = _serviceProvider.GetService<RegisterVMValidation>().Validate(model);
            Dictionary<string, string> errors;
            if (!validationResult.IsValid)
                errors = validationResult.ToErrorModel();
            else
            {
                var val = new ApplicationUser()
                {
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    UserName = model.Email,
                    Name = model.Name,
                    Surname = model.Surname
                };
                var result = await _userManager.CreateAsync(val, model.Password);

                if (result.Succeeded)
                {
                    result = await _userManager.AddToRoleAsync(val, "User");
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(val, true);
                        return Ok();
                    }
                }
                errors = result.ToErrorModel();
            }
            return BadRequest(errors);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var validationResult = _serviceProvider.GetService<LoginVmValidation>().Validate(model);
            Dictionary<string, string> errors;
            if (!validationResult.IsValid)
                errors = validationResult.ToErrorModel();
            else
            {
                var result=await _signInManager.PasswordSignInAsync(model.Email,model.Password,true,true);
                if (result.Succeeded)
                    return Ok();
                errors = result.ToErrorModel();
            }
            return BadRequest(errors);
        }
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user==null)
                return Ok();
            var returnValue = _mapper.Map<ApplicationUser, GetUserVM>(user);
            returnValue.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            return Ok(returnValue);
        }
    }
}
