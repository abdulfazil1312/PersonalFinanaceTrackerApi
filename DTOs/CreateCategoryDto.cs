using ExpenseTrackerAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTrackerAPI.DTOs
{
    public class CreateCategoryDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } // e.g., "Groceries", "Salary"

        [Required]
        public CategoryType Type { get; set; } // Expense or Income
    }
}
