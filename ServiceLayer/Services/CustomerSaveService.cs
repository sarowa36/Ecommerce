using DataAccessLayer.Base.Repositories.CustomerSaveItemRepositories;
using DataAccessLayer.Base.Repositories.CustomerSaveRepositories;
using DataAccessLayer.Base.Repositories.ProductRepositories;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Base;

namespace ServiceLayer.Services
{
    public class CustomerSaveService
    {
        readonly ICustomerSaveWriteRepository _customerSaveWriteRepository;
        readonly ICustomerSaveReadRepository _customerSaveReadRepository;
        readonly ICustomerSaveItemReadRepository _customerSaveItemReadRepository;
        readonly ICustomerSaveItemWriteRepository _customerSaveItemWriteRepository;
        readonly IProductReadRepository _productReadRepository;
        readonly IServiceErrorContainer _errorContainer;
        public CustomerSaveService(ICustomerSaveWriteRepository customerSaveRepository, ICustomerSaveReadRepository customerSaveReadRepository, IServiceErrorContainer errorContainer, ICustomerSaveItemReadRepository customerSaveItemRepository, ICustomerSaveItemWriteRepository customerSaveItemWriteRepository, IProductReadRepository productReadRepository)
        {
            _customerSaveWriteRepository = customerSaveRepository;
            _customerSaveReadRepository = customerSaveReadRepository;
            _errorContainer = errorContainer;
            _customerSaveItemReadRepository = customerSaveItemRepository;
            _customerSaveItemWriteRepository = customerSaveItemWriteRepository;
            _productReadRepository = productReadRepository;
        }

        public async Task AddItem(string userId, int saveId, int productId)
        {
            if (_customerSaveItemReadRepository.Table.Any(x => x.CustomerSaveId == saveId && x.ProductId == productId)) 
            {
                _errorContainer.AddModelOnlyError("Ürün listede mevcut");
                return; 
            }
            if(!_productReadRepository.Exist(x=>x.Id==productId))
            {
                _errorContainer.AddModelOnlyError("Ürün bulunamadı");
                return;
            }
            if (!_customerSaveReadRepository.Exist(x => x.Id == saveId))
            {
                _errorContainer.AddModelOnlyError("Liste bulunamadı");
                return;
            }
            await _customerSaveItemWriteRepository.CreateAsync(new CustomerSaveItem() { CustomerSaveId=saveId,ProductId=productId});
            await _customerSaveItemWriteRepository.SaveChangesAsync();
        }
        public async Task Create(CustomerSave customerSave)
        {
            if (_customerSaveReadRepository.Table.Any(x => x.Name == customerSave.Name && x.UserId == customerSave.UserId))
            {
                _errorContainer.AddError("name", "Aynı ada sahip bir liste mevcut");
                return;
            }
            if (_customerSaveReadRepository.Table.Count(x => x.UserId == customerSave.UserId) > 12)
            {
                _errorContainer.AddModelOnlyError("En fazla 12 adet liste oluşturabilirsin");
                return;
            }
            await _customerSaveWriteRepository.CreateAsync(customerSave);
            await _customerSaveWriteRepository.SaveChangesAsync();
        }
        public async Task<CustomerSave> Get(int id, string userId)
        {
            var ent = _customerSaveReadRepository.Table.FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (ent == null)
            {
                _errorContainer.AddModelOnlyError("Böyle bir liste mevcut değil");
                return ent;
            }
            return ent;
        }
        public async Task<CustomerSave> GetWithItems(int id, string userId)
        {
            var ent = _customerSaveReadRepository.Table.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == id && x.UserId == userId);
            if (ent == null)
            {
                _errorContainer.AddModelOnlyError("Böyle bir liste mevcut değil");
                return ent;
            }
            return ent;
        }
        public async Task<List<CustomerSave>> GetList(string userId)
        {
            return _customerSaveReadRepository.Table.Where(x => x.UserId == userId).ToList(); ;
        }
        public async Task<List<CustomerSave>> GetListWithItems(string userId)
        {
            return _customerSaveReadRepository.Table.Include(x => x.Items).ThenInclude(x => x.Product).Where(x => x.UserId == userId).ToList(); ;
        }
        public async Task Update(CustomerSave customerSave)
        {
            if (_customerSaveReadRepository.Table.Any(x =>x.Id!=customerSave.Id && x.Name == customerSave.Name && x.UserId == customerSave.UserId))
            {
                _errorContainer.AddError("name", "Aynı ada sahip bir liste mevcut");
                return;
            }
            await _customerSaveWriteRepository.UpdateAsync(customerSave);
            await _customerSaveWriteRepository.SaveChangesAsync();
        }
        public async Task Delete(int id, string userId)
        {
            var isExist = _customerSaveReadRepository.Table.Any(x => x.Id == id && x.UserId == userId);
            if (!isExist)
            {
                _errorContainer.AddModelOnlyError("Böyle bir liste mevcut değil");
                return;
            }
            await _customerSaveWriteRepository.DeleteAsync(id);
            await _customerSaveWriteRepository.SaveChangesAsync();
        }
        public async Task DeleteItem(string userId, int saveId, int productId)
        {
            var item=_customerSaveItemReadRepository.GetWhere(x => x.CustomerSaveId == saveId && x.ProductId == productId && x.CustomerSave.UserId == userId);
            if (item != null) { 
            await _customerSaveItemWriteRepository.DeleteAsync(item);
            await _customerSaveItemWriteRepository.SaveChangesAsync();
            }
            else
            {
                _errorContainer.AddModelOnlyError("Object doesnt exist");
            }
        }
    }
}
