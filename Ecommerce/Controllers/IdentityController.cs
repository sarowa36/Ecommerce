using AutoMapper;
using BusinessLayer.Validations.IdentityController;
using DataAccessLayer.Repositories.ShoppingCartItemRepositories;
using Ecommerce.AutoMappers.IdentityController;
using ToolsLayer.ErrorModel;
using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceLayer.Base.Services;
using DataAccessLayer.Base.Repositories.ShoppingCartItemRepositories;
using ServiceLayer.ServiceResults.IdentityService;
using Azure;

namespace Ecommerce.Controllers
{
    public class IdentityController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IShoppingCartItemWriteRepository _shoppingCartItemWriteRepository;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        readonly IShoppingCartService _shoppingCartService;
        public IdentityController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IShoppingCartItemWriteRepository shoppingCartItemWriteRepository, IIdentityService identityService, IShoppingCartService shoppingCartService)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _identityService = identityService;
            _shoppingCartService = shoppingCartService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var validationResult = _serviceProvider.GetService<RegisterVMValidation>().Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToErrorModel());
            else
            {
                var response = await _identityService.CreateUserAsync(model);

                if (response.IsSuccess)
                {
                    response.BindResponse(await _identityService.LoginAsync(response.Value));
                    response.BindResponse(await _shoppingCartService.CookieCartConvertToDbCartAsync(response.Value));
                }
                return response.IsSuccess ? Ok() : BadRequest(response.Errors);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var validationResult = _serviceProvider.GetService<LoginVmValidation>().Validate(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToErrorModel());
            else
            {
                var loginResponse = await _identityService.LoginAsync(model.Email, model.Password);
                if (loginResponse.IsSuccess)
                {
                    loginResponse.BindResponse(await _shoppingCartService.CookieCartConvertToDbCartAsync(loginResponse.Value));
                }
                return loginResponse.IsSuccess ? Ok() : BadRequest(loginResponse.Errors);
            }
        }
        public async Task<IActionResult> GetUser()
        {
            var getCurrentUserResponse = await _identityService.GetCurrentUserAsync();
            GetUserVM? returnValue=null;
            if (getCurrentUserResponse.IsSuccess)
            {
                returnValue = _mapper.Map<ApplicationUser, GetUserVM>(getCurrentUserResponse.Value);
                var getUserRolesResponse=await _identityService.GetUserRolesAsync();
                getCurrentUserResponse.BindResponse(getUserRolesResponse);
                returnValue.Roles = getUserRolesResponse.Value;
            }
            return getCurrentUserResponse.IsSuccess ? Ok(returnValue) :BadRequest(getCurrentUserResponse.Errors);
        }
    }
}
