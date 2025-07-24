using ExpenseTrackerApi.Models;
using ExpenseTrackerAPI.Data;
using ExpenseTrackerAPI.DTOs;
using ExpenseTrackerAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExpenseTrackerAPI.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(ICategoryRepository categoryRepository, IHttpContextAccessor httpContextAccessor)
        {
            _categoryRepository = categoryRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userId);
        }

        public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
        {
            var userId = GetCurrentUserId();
            var categories = await _categoryRepository.GetCategoriesByUserIdAsync(userId);

            return categories.Select(c => new CategoryDto
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                Type = c.Type
            });
        }

        public async Task<Category> CreateCategoryAsync(CreateCategoryDto categoryDto)
        {
            var userId = GetCurrentUserId();
            var category = new Category
            {
                UserId = userId,
                Name = categoryDto.Name,
                Type = categoryDto.Type
            };

            return await _categoryRepository.CreateCategoryAsync(category);
        }
    }
}
