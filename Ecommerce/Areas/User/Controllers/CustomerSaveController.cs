using AutoMapper;
using BusinessLayer.Validations.User.CustomerSaveController;
using EntityLayer.DTOs.Areas.User.CustomerSaveController;
using EntityLayer.Entities;
using EntityLayer.ViewModels.User.CustomerSaveController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;
using ServiceLayer.Services;

namespace Ecommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "User")]
    public class CustomerSaveController : Controller
    {
        readonly CustomerSaveService _service;
        readonly IServiceErrorContainer _errorContainer;
        readonly IServiceProvider _serviceProvider;
        readonly IIdentityService _identityService;
        readonly IMapper _mapper;
        public CustomerSaveController(CustomerSaveService service, IServiceErrorContainer errorContainer, IMapper mapper, IServiceProvider serviceProvider, IIdentityService identityService)
        {
            this._service = service;
            _errorContainer = errorContainer;
            _mapper = mapper;
            _serviceProvider = serviceProvider;
            _identityService = identityService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerSaveDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<CreateCustomerSaveDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var ent = _mapper.Map<CustomerSave>(model);
            if (_errorContainer.IsSuccess)
                ent.UserId = user.Id;
            _errorContainer.AddServiceResponse(() => _service.Create(ent));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(CreateCustomerSaveItemDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<CreateCustomerSaveItemDTOValidation>().Validate(model));
            var user=_errorContainer.AddServiceResponse(()=>_identityService.GetCurrentUserAsync());
            _errorContainer.AddServiceResponse(() => _service.AddItem(user.Id, model.CustomerSaveId, model.ProductId));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> Get(int id)
        {
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var ent = _errorContainer.AddServiceResponse(() => _service.GetWithItems(id, user.Id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<CustomerSaveVM>( ent)) : BadRequest(_errorContainer.Errors);
        }
        public async Task<IActionResult> GetList()
        {
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var ent = _errorContainer.AddServiceResponse(() => _service.GetListWithItems(user.Id));
            return _errorContainer.IsSuccess ? Ok(_mapper.Map<List<CustomerSaveVM>>(ent)) : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerSaveDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<UpdateCustomerSaveDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            var ent = _errorContainer.AddServiceResponse(() => _service.Get(model.Id, user.Id));
            if (_errorContainer.IsSuccess)
                ent.UserId = user.Id;
            _mapper.Map(model, ent);
            _errorContainer.AddServiceResponse(() => _service.Update(ent));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCustomerSaveDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<DeleteCustomerSaveDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            _errorContainer.AddServiceResponse(() => _service.Delete(model.SaveId, user.Id));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteItem(DeleteCustomerSaveItemDTO model)
        {
            _errorContainer.BindValidation(_serviceProvider.GetService<DeleteCustomerSaveItemDTOValidation>().Validate(model));
            var user = _errorContainer.AddServiceResponse(() => _identityService.GetCurrentUserAsync());
            _errorContainer.AddServiceResponse(() => _service.DeleteItem( user.Id, model.SaveId,model.ProductId));
            return _errorContainer.IsSuccess ? Ok() : BadRequest(_errorContainer.Errors);
        }
    }
}
