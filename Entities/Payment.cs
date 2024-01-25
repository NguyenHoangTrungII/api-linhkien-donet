using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace linhkien_donet.Entities
{
    public class Payment
    {
        public string PaymentId { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool PaymentStatus { get; set; }

        public string Token { get; set; }

        public string TransactionId { get; set; }
        public string Content { get; set; }

        public string PaymentResponseCode { get; set; }


        public int  OrderId { get; set; }
        public  Order Orders { get; set; }
    }
}
