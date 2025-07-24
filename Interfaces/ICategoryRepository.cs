using ExpenseTrackerApi.Models;

namespace ExpenseTrackerAPI.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
        Task<Category> CreateCategoryAsync(Category category);
    }
}
