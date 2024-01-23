using DataAccessLayer.Base.Repositories.OrderItemRepositories;
using DataAccessLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Entities;

namespace DataAccessLayer.Repositories.OrderItemRepositories
{
    public class OrderItemReadRepository : AbstractGenericReadRepository<OrderItem>, IOrderItemReadRepository
    {
        public OrderItemReadRepository(ADC db) : base(db)
        {
        }
    }
}
