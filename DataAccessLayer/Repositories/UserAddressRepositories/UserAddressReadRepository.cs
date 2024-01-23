using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.UserAddressRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.UserAddressRepositories
{
    public class UserAddressReadRepository : AbstractGenericReadRepository<UserAddress>, IUserAddressReadRepository
    {
        public UserAddressReadRepository(ADC db) : base(db)
        {
        }
    }
}
