using BlazorApp.Data;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
{
    public class ChildrenService : IChildService
    {
        private readonly ApplicationDbContext _context;
        public ChildrenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Child>> GetChildrenByParentIdAsync(string parentId)
        {
            return await _context.Children
                .Where(c => c.ParentId == parentId)
                .ToListAsync();
        }
        public async Task<bool> AddChildAsync(Child child)
        {
            _context.Children.Add(child);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
