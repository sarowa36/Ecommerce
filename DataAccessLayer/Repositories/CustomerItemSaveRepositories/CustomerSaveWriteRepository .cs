using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CustomerSaveItemRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CustomerSaveRepositories
{
    public class CustomerSaveItemWriteRepository : AbstractGenericWriteRepository<CustomerSaveItem>, ICustomerSaveItemWriteRepository
    {
        public CustomerSaveItemWriteRepository(ADC db) : base(db)
        {
        }
    }
}
