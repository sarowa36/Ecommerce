using AutoMapper;
using BusinessLayer.Validations.IdentityController;
using EntityLayer.DTOs.Controllers.IdentityController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.IdentityController;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        readonly IShoppingCartService _shoppingCartService;
        readonly IServiceErrorContainer _serviceErrorContainer;
        readonly IRoleService _authService;
        public IdentityController(IServiceProvider serviceProvider,
            IMapper mapper,
            IIdentityService identityService,
            IShoppingCartService shoppingCartService,
            IServiceErrorContainer serviceErrorProvider,
            IRoleService authService)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
            _identityService = identityService;
            _shoppingCartService = shoppingCartService;
            _serviceErrorContainer = serviceErrorProvider;
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            _serviceErrorContainer.BindValidation(_serviceProvider.GetService<RegisterDTOValidation>().Validate(model));

            var user = new ApplicationUser()
            {
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname
            };
            _serviceErrorContainer.AddServiceResponse(() => _identityService.CreateUserAsync(user, model.Password));
            _serviceErrorContainer.AddServiceResponse(() => _authService.AssignUserRole(user));
            _serviceErrorContainer.AddServiceResponse(() => _identityService.LoginAsync(user));
            _serviceErrorContainer.AddServiceResponse(() => _shoppingCartService.CookieCartConvertToDbCartAsync(user));
            return _serviceErrorContainer.IsSuccess ? Ok() : BadRequest(_serviceErrorContainer.Errors);

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            _serviceErrorContainer.BindValidation(_serviceProvider.GetService<LoginDTOValidation>().Validate(model));
            var user = _serviceErrorContainer.AddServiceResponse(() => _identityService.GetUser(model.Email));
            _serviceErrorContainer.AddServiceResponse(() => _identityService.LoginAsync(user, model.Password));
            _serviceErrorContainer.AddServiceResponse(() => _shoppingCartService.CookieCartConvertToDbCartAsync(user));

            return _serviceErrorContainer.IsSuccess ? Ok() : BadRequest(_serviceErrorContainer.Errors);
        }
        public async Task<IActionResult> GetUser()
        {
            var user = _serviceErrorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var roles = _serviceErrorContainer.AddServiceResponse(() => _authService.GetUserRolesAsync(user));
            if (_serviceErrorContainer.IsSuccess)
            {
                var returnValue = _mapper.Map<ApplicationUser, GetUserVM>(user);
                returnValue.Roles = roles;
                return Ok(returnValue);
            }
            return Ok();
        }
    }
}
