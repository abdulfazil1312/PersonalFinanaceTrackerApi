using ExpenseTrackerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Salary", "Groceries", "Transport"

        [Required]
        public CategoryType Type { get; set; } // Is this an "Expense" or "Income" category?

        // Foreign Key to the User
        public int UserId { get; set; }
        public User User { get; set; } // Navigation property
    }
}
