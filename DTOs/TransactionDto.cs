using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.DTOs
{
    public class TransactionDto
    {
        public int TransactionId { get; set; }
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; }
        public string? CategoryName { get; set; }
        public string? AccountName { get; set; }
        public string? FromAccountName { get; set; }
        public string? ToAccountName { get; set; }
    }
}
