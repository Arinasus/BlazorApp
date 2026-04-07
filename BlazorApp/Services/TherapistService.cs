using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using BlazorApp.Shared.Models;
namespace BlazorApp.Services
{
    public class TherapistService
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
    }
}
