namespace ExpenseTrackerAPI.DTOs
{
    public class AccountDto
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string Currency { get; set; }
    }
}
