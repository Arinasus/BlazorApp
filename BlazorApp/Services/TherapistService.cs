using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using BlazorApp.Shared.Models;
using BlazorApp.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp.Services
{
    public class TherapistService : ITherapistService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TherapistService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task CreateApplicationAsync(TherapistProfile profile)
        {
            profile.IsApproved = false;
            profile.CreatedAt = DateTime.UtcNow;

            _context.TherapistProfiles.Add(profile);
            await _context.SaveChangesAsync();
        }

        public async Task ApproveTherapistAsync(int profileId)
        {
            var profile = await _context.TherapistProfiles.FindAsync(profileId);
            if (profile != null)
            {
                profile.IsApproved = true;

                var user = await _userManager.FindByIdAsync(profile.UserId);
                if (user != null)
                {
                    await _userManager.AddToRoleAsync(user, "Therapist");
                }
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<TherapistProfile>> GetPendingApplicationsAsync()
        {
            return await _context.TherapistProfiles
                .Where(p => !p.IsApproved)
                .ToListAsync();
        }
        public async Task<TherapistProfile?> GetProfileByUserId(string userId)
        {
            return await _context.TherapistProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateProfile(TherapistProfile profile)
        {
            var existing = await _context.TherapistProfiles
                .FirstOrDefaultAsync(p => p.UserId == profile.UserId);

            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(profile);
            }
            else
            {
                _context.TherapistProfiles.Add(profile);
            }
            var affectedRows = await _context.SaveChangesAsync();
            Console.WriteLine($"DB UPDATE: Изменено строк: {affectedRows}");
        }
        public async Task<List<TherapistProfile>> GetApprovedProfilesAsync()
        {
            return await _context.TherapistProfiles
                .Include(p => p.Reviews)
                .Where(p => p.IsApproved)
                .Select(p => new TherapistProfile
                {
                    Id = p.Id,
                    UserId = p.UserId,
                    FirstName = p.FirstName ?? "",
                    LastName = p.LastName ?? "",
                    MiddleName = p.MiddleName ?? "",
                    Specialization = p.Specialization ?? "",
                    ShortDescription = p.ShortDescription ?? "",
                    WorkFormat = p.WorkFormat ?? "Online",
                    ImageUrl = p.ImageUrl ?? "/img/default-avatar.png",
                    PricePerHour = p.PricePerHour,
                    Reviews = p.Reviews
                })
                .ToListAsync();
        }
        public async Task<List<TherapistReview>> GetReviewsByTherapistIdAsync(int therapistProfileId)
        {
            return await _context.TherapistReviews
                .Where(r => r.TherapistProfileId == therapistProfileId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<bool> AddReviewAsync(TherapistReview review)
        {
            try
            {
                _context.TherapistReviews.Add(review);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
