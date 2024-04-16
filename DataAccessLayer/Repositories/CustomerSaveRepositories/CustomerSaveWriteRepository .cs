using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CustomerSaveRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CustomerSaveRepositories
{
    public class CustomerSaveWriteRepository : AbstractGenericWriteRepository<CustomerSave>, ICustomerSaveWriteRepository
    {
        public CustomerSaveWriteRepository(ADC db) : base(db)
        {
        }
    }
}
