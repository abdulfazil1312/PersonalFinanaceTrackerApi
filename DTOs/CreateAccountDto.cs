using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class CreateAccountDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Savings Account", "Cash"

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // e.g., "Bank", "Credit Card", "Cash"

        [Required]
        [MaxLength(10)]
        public string Currency { get; set; } // e.g., "INR", "USD"
    }
}
