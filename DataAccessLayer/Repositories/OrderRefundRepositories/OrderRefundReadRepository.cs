using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.OrderRefundRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.OrderRefundRepositories
{
    public class OrderRefundReadRepository : AbstractGenericReadRepository<OrderRefund>, IOrderRefundReadRepository
    {
        public OrderRefundReadRepository(ADC db) : base(db)
        {
        }
    }
}
