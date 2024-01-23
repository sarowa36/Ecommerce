using EntityLayer.M2M;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class OrderRefund : IIntIdentity, ICreateDate
    {
        public int Id { get; set; }
        public int TotalRefundAmount { get; set; }
        public List<OrderItem> OrderItems {  get; set; }
        public DateTime CreateDate { get; set; }
        public List<OrderRefundOrderItem> OrderRefundOrderItems { get; set; }
    }
}
