using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.Integrations.Iyzipay
{
    public class IyziWebhookDTO
    {
        public int MerchantId { get; set; }
        public string Token { get; set; }
        public string Status { get; set; }
        public string IyziReferenceCode { get; set; }
        public string IyziEventType { get; set; }
        public string IyziEventTime { get; set; }
        public string IyziPaymentId { get; set; }
    }
}
