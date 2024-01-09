using EntityLayer.Entities;

namespace ServiceLayer.Base.Services
{
    public interface IUserAddressService
    {
        Task CreateAddress(UserAddress userAddress);
        Task<UserAddress> Get(ApplicationUser user, int addressId);
        Task<List<UserAddress>> GetList(ApplicationUser user);
        Task UpdateAddress(UserAddress userAddress);
    }
}