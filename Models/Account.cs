using ExpenseTrackerAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerApi.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Savings Account", "Cash"

        [Required]
        [MaxLength(50)]
        public string Type { get; set; } // e.g., "Bank", "Credit Card", "Cash"

        [Required]
        [MaxLength(10)]
        public string Currency { get; set; } // e.g., "INR", "USD"

        [Column(TypeName = "decimal(18, 2)")]
        public decimal InitialBalance { get; set; } = 0;

        // Foreign Key to the User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
    }
}