using System.ComponentModel.DataAnnotations.Schema;

namespace RapidPay.Models
{
    [Table("Transactions")]
    public class Transaction
    {
        public int Id { get; set; }
        public int cardId { get; set; }
        public decimal transactionFee { get; set; }
        public decimal transactionAmount { get; set; }
        public DateTime dateCreated { get; set; }
    }

}
