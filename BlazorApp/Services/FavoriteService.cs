using BlazorApp.Data;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _context;
        public FavoriteService( ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ToggleFavoriteAsync(string userId, int? lectureId = null, int? therapistId = null)
        {
            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId &&
                                         f.LectureId == lectureId &&
                                         f.TherapistProfileId == therapistId);

            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);
            }
            else
            {
                _context.Favorites.Add(new Favorite
                {
                    UserId = userId,
                    LectureId = lectureId,
                    TherapistProfileId = therapistId
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.TherapistProfile) 
                    .ThenInclude(p => p != null ? p.Reviews : null) 
                .Include(f => f.Lecture) 
                .ToListAsync();
        }

        public async Task<bool> IsFavoriteAsync(string userId, int? lectureId = null, int? therapistId = null)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId &&
                              f.LectureId == lectureId &&
                              f.TherapistProfileId == therapistId);
        }
    
    }
}
