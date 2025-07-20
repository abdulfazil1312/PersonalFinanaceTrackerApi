using ExpenseTrackerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        public TransactionType Type { get; set; }

        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Note { get; set; }

        // For Expense/Income
        public int? CategoryId { get; set; }
        public int? AccountId { get; set; }

        // For Transfers
        public int? FromAccountId { get; set; }
        public int? ToAccountId { get; set; }
    }
}
