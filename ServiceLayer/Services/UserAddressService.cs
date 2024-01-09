using DataAccessLayer.Base.Repositories.UserAddressRepositories;
using EntityLayer.Entities;
using ServiceLayer.Base;
using ServiceLayer.Base.Services;

namespace ServiceLayer.Services
{
    public class UserAddressService : IUserAddressService
    {
        readonly IServiceErrorContainer _errorContainer;
        readonly IUserAddressReadRepository _userAddressReadRepository;
        readonly IUserAddressWriteRepository _userAddressWriteRepository;
        public UserAddressService(IServiceErrorContainer serviceContainer, IUserAddressWriteRepository userAddressWriteRepository, IUserAddressReadRepository userAddressReadRepository)
        {
            _errorContainer = serviceContainer;
            _userAddressWriteRepository = userAddressWriteRepository;
            _userAddressReadRepository = userAddressReadRepository;
        }
        public async Task CreateAddress(UserAddress userAddress)
        {
            if (_userAddressReadRepository.Exist(x => x.Name == userAddress.Name && x.UserId == userAddress.UserId))
            {
                _errorContainer.AddError("ModelOnly", "Address Name already using");
            }
            else
            {
                await _userAddressWriteRepository.CreateAsync(userAddress);
                await _userAddressWriteRepository.SaveChangesAsync();
            }
        }
        public async Task UpdateAddress(UserAddress userAddress)
        {
            if (_userAddressReadRepository.Exist(x => x.Name == userAddress.Name && x.UserId == userAddress.UserId && x.Id != userAddress.Id))
            {
                _errorContainer.AddError("ModelOnly", "Address Name already using");
            }
            else
            {
                await _userAddressWriteRepository.UpdateAsync(userAddress);
                await _userAddressWriteRepository.SaveChangesAsync();
            }
        }
        public async Task<List<UserAddress>> GetList(ApplicationUser user)
        {
            return _userAddressReadRepository.GetAll().Where(x => x.UserId == user.Id).ToList();
        }
        public async Task<UserAddress> Get(ApplicationUser user, int addressId)
        {
            if (_userAddressReadRepository.GetWhere(x => x.Id == addressId && x.UserId == user.Id, out var address))
            {
                return address;
            }
            else
            {
                _errorContainer.AddError("ModelOnly", "Address not found");
                return null;
            }
        }
    }
}
