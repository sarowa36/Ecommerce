using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CustomerSaveItemRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CustomerSaveItemRepositories
{
    public class CustomerSaveItemReadRepository : AbstractGenericReadRepository<CustomerSaveItem>, ICustomerSaveItemReadRepository
    {
        public CustomerSaveItemReadRepository(ADC db) : base(db)
        {
        }
    }
}
