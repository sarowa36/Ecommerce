using DataAccessLayer.Base;
using DataAccessLayer.Base.Repositories.OrderItemRepositories;
using EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.OrderItemRepositories
{
    public class OrderItemWriteRepository : AbstractGenericWriteRepository<OrderItem>, IOrderItemWriteRepository
    {
        public OrderItemWriteRepository(ADC db) : base(db)
        {
        }
    }
}
