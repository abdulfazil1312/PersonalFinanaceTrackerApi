namespace ExpenseTrackerAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Titile { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }

    }
}
