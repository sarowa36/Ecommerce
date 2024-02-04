using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.OrderRefundRepositories;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.OrderRefundRepositories
{
    public class OrderRefundWriteRepository : AbstractGenericWriteRepository<OrderRefund>, IOrderRefundWriteRepository
    {
        public OrderRefundWriteRepository(ADC db) : base(db)
        {
        }
    }
}
