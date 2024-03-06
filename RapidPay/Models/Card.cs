using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Models
{
    [Table("Cards")]
    public class Card
    {
        public int Id { get; set; }
        public string cardNumber { get; set; }
        public decimal balance { get; set; }
        public string last4digits { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateUpdated { get; set; }
    }
}
