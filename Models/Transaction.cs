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
        public User User { get; set; }

        // --- Fields for Expense/Income ---
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // --- NEW Fields for Transfers ---
        [MaxLength(100)]
        public string? TransferFrom { get; set; }

        [MaxLength(100)]
        public string? TransferTo { get; set; }
    }
}
