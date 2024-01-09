using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.OrderRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.OrderRepositories
{
    public class OrderWriteRepository : AbstractGenericWriteRepository<Order>,IOrderWriteRepository
    {
        public OrderWriteRepository(ADC db) : base(db)
        {
        }
    }
}
