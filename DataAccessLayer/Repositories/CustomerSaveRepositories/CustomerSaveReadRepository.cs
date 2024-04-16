using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.CustomerSaveRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.CustomerSaveRepositories
{
    public class CustomerSaveReadRepository : AbstractGenericReadRepository<CustomerSave>, ICustomerSaveReadRepository
    {
        public CustomerSaveReadRepository(ADC db) : base(db)
        {
        }
    }
}
