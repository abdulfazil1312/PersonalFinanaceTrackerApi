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

        // For Transfers
        public string? TransferFrom { get; set; }
        public string? TransferTo { get; set; }
    }
}
