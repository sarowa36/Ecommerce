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
using ServiceLayer.ServiceResults.Identity;

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
        public IdentityController(UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider, RoleManager<IdentityRole> roleManager, SignInManager<ApplicationUser> signInManager, IMapper mapper, IShoppingCartItemWriteRepository shoppingCartItemWriteRepository, IIdentityService identityService)
        {
            _userManager = userManager;
            _serviceProvider = serviceProvider;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _shoppingCartItemWriteRepository = shoppingCartItemWriteRepository;
            _identityService = identityService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            var validationResult = _serviceProvider.GetService<RegisterVMValidation>().Validate(model);
            Dictionary<string, string> errors;
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToErrorModel());
            else
            {
                var createUserResponse = await _identityService.CreateUserAsync(model);

                if (createUserResponse.IsSuccess)
                {
                        await _signInManager.SignInAsync(createUserResponse.Value, true);
                        if (HttpContext.Request.Cookies.TryGetValue("shoppingCart",out var cookieCartString))
                        {
                            var cartProductAndQuantity=JsonConvert.DeserializeObject<Dictionary<int,int>>(cookieCartString);
                            if(cartProductAndQuantity != null && cartProductAndQuantity.Count>0) {
                                await _shoppingCartItemWriteRepository.UpdateRangeAsync(cartProductAndQuantity.Select(x=>new ShoppingCartItem() {UserId= createUserResponse.Value.Id, ProductId=x.Key,Quantity=x.Value}));
                                await _shoppingCartItemWriteRepository.SaveChangesAsync();
                            }
                            HttpContext.Response.Cookies.Delete("shoppingCart");
                        }
                        return Ok();
                }
                return BadRequest(createUserResponse.Errors);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var validationResult = _serviceProvider.GetService<LoginVmValidation>().Validate(model);
            Dictionary<string, string> errors;
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToErrorModel());
            else
            {
                var loginResponse=await _identityService.LoginAsync(model.Email,model.Password);
                if(loginResponse.IsSuccess && HttpContext.Request.Cookies.TryGetValue("shoppingCart", out var cookieCartString))
                {
                    var cartProductAndQuantity = JsonConvert.DeserializeObject<Dictionary<int, int>>(cookieCartString);
                    if (cartProductAndQuantity != null && cartProductAndQuantity.Count > 0)
                    {
                        await _shoppingCartItemWriteRepository.UpdateRangeAsync(cartProductAndQuantity.Select(x => new ShoppingCartItem() { UserId = loginResponse.Value.Id, ProductId = x.Key, Quantity = x.Value }));
                        await _shoppingCartItemWriteRepository.SaveChangesAsync();
                    }
                    HttpContext.Response.Cookies.Delete("shoppingCart");
                }
                return loginResponse.IsSuccess ? Ok() : BadRequest(loginResponse.Errors);
            }
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
