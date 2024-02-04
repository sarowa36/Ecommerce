using AutoMapper;
using BusinessLayer.Validations.User.AddressController;
using EntityLayer.DTOs.Areas.User.AddressController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.AddressController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class AddressController : Controller
    {
        readonly IUserAddressService _userAdressService;
        readonly IServiceErrorContainer _errorContainer;
        readonly IServiceProvider _serviceProvider;
        readonly IIdentityService _identityService;
        readonly IMapper _mapper;
        public AddressController(IUserAddressService userAdressService,
            IServiceErrorContainer errorContainer,
            IServiceProvider serviceProvider,
            IIdentityService identityService,
            IMapper mapper)
        {
            _userAdressService = userAdressService;
            _errorContainer = errorContainer;
            _serviceProvider = serviceProvider;
            _identityService = identityService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateAddressDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<CreateAddressDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            UserAddress address = new();
            if (_errorContainer.IsSuccess)
            {
                address = _mapper.Map<UserAddress>(model);
                address.User = user;
                address.UserId = user.Id;
            }
            _errorContainer.AddServiceResponse(() => _userAdressService.CreateAddress(address));
            return _errorContainer.IsSuccess ? Ok(new { id = address.Id }) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateAddressDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<UpdateAddressDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            UserAddress address = new();
            if (_errorContainer.IsSuccess)
            {
                address = _mapper.Map<UserAddress>(model);
                address.UserId = user.Id;
            }
            _errorContainer.AddServiceResponse(() => _userAdressService.UpdateAddress(address));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var addresses = _errorContainer.AddServiceResponse(() => _userAdressService.GetList(user));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<List<AddressListValueVM>>(addresses)) : BadRequest(_errorContainer.Errors);
        }
    }
}
