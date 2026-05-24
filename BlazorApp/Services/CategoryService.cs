using BlazorApp.Data;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context) => _context = context;

        public async Task<List<Category>> GetAllCategoriesAsync() => await _context.Categories.ToListAsync();

        public async Task<bool> AddCategoryAsync(Category category)
        {
            var newNameLower = category.Name.Trim().ToLower();

            if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == newNameLower))
            {
                return false; 
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return true; 
        }
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null && !category.IsSystem) 
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
