using System.ComponentModel.DataAnnotations;

namespace RapidPay.DTOs
{
    public class UpdateCardDto
    {

        [Required]
        [RegularExpression(@"^\d{15}$", ErrorMessage = "cardNumber should be 15 digits")]
        public string cardNumber { get; set; }

        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Invalid amount format, please ensure it has a maximum of two decimals")]
        public decimal amount { get; set; }
    }
}
