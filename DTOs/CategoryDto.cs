using ExpenseTrackerAPI.Models;

namespace ExpenseTrackerAPI.DTOs
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
    }
}
