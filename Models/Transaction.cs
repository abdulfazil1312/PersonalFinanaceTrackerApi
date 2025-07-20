using ExpenseTrackerApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTrackerAPI.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public TransactionType Type { get; set; } // Expense, Income, or Transfer

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [MaxLength(500)]
        public string? Note { get; set; }

        // Foreign Key to the User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property

        // --- Fields for Expense/Income ---
        // Nullable because they don't apply to Transfers
        public int? CategoryId { get; set; }
        public Category? Category { get; set; } // Navigation property

        public int? AccountId { get; set; } // The account that was affected
        public Account? Account { get; set; } // Navigation property

        // --- Fields for Transfers ---
        // Nullable because they only apply to Transfers
        public int? FromAccountId { get; set; }
        public Account? FromAccount { get; set; } // Navigation property

        public int? ToAccountId { get; set; }
        public Account? ToAccount { get; set; } // Navigation property
    }
}
