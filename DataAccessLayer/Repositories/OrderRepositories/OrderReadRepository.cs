using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.OrderRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.OrderRepositories
{
    public class OrderReadRepository : AbstractGenericReadRepository<Order>,IOrderReadRepository
    {
        public OrderReadRepository(ADC db) : base(db)
        {
        }
    }
}
