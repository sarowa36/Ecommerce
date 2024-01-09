using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.UserAddressRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.UserOrderRepositories
{
    public class UserAddressWriteRepository : AbstractGenericWriteRepository<UserAddress>, IUserAddressWriteRepository
    {
        public UserAddressWriteRepository(ADC db) : base(db)
        {
        }
    }
}
