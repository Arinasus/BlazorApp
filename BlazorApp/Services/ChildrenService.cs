using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
{
    public class ChildrenService
    {
        private readonly ApplicationDbContext _context;
        public ChildrenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Child>> GetChildrenForParentAsync(string parentId)
        {
            return await _context.Children
                .Where(c => c.ParentId == parentId)
                .ToListAsync();
        }
    }
}
