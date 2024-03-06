

using System.ComponentModel.DataAnnotations;

namespace RapidPay.DTOs
{
    public class CardDto
    {
        [Required]
        [RegularExpression(@"^\d{15}$", ErrorMessage = "cardNumber should be 15 digits")]
        public string cardNumber { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid balance format")]
        public decimal balance { get; set; }
    }
}
