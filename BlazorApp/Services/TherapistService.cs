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
                profile.IsModerationRequired = false;

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
        .Where(p => !p.IsApproved || p.IsModerationRequired)
        .ToListAsync();
        }
        public async Task UpdateVerificationStatusAsync(int profileId, bool isPassport, bool isEducation, bool isSelfEmployed)
        {
            var profile = await _context.TherapistProfiles.FindAsync(profileId);
            if (profile != null)
            {
                profile.IsPassportVerified = isPassport;
                profile.IsEducationVerified = isEducation;
                profile.IsSelfEmployed = isSelfEmployed;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<TherapistProfile?> GetProfileByUserId(string userId)
        {
            return await _context.TherapistProfiles
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task UpdateProfile(TherapistProfile profile)
        {
            if (profile == null || string.IsNullOrWhiteSpace(profile.UserId))
                throw new ArgumentException("Invalid profile or UserId");

            var existing = await _context.TherapistProfiles
                .FirstOrDefaultAsync(p => p.UserId == profile.UserId);

            if (existing == null)
            {
                // Новый профиль
                profile.IsApproved = false;
                profile.CreatedAt = DateTime.UtcNow;

                _context.TherapistProfiles.Add(profile);
            }
            else
            {
                existing.FirstName = profile.FirstName;
                existing.LastName = profile.LastName;
                existing.MiddleName = profile.MiddleName;
                existing.Specialization = profile.Specialization;
                existing.ExperienceYears = profile.ExperienceYears;
                existing.PricePerHour = profile.PricePerHour;
                existing.WorkFormat = profile.WorkFormat;
                existing.Phone = profile.Phone;
                existing.City = profile.City;
                existing.Address = profile.Address;
                existing.Education = profile.Education;
                existing.FullBio = profile.FullBio;
                existing.ShortDescription = profile.ShortDescription;
                existing.IsPassportVerified = profile.IsPassportVerified;
                existing.IsEducationVerified = profile.IsEducationVerified;
                existing.IsSelfEmployed = profile.IsSelfEmployed;
                existing.SpeechDisorders = profile.SpeechDisorders;
                existing.SpecialNeeds = profile.SpecialNeeds;
                existing.WorkType = profile.WorkType;
                existing.SpeechDisorders = profile.SpeechDisorders;
                existing.SpecialNeeds = profile.SpecialNeeds;
                existing.WorkType = profile.WorkType;
                existing.IsSelfEmployed = profile.IsSelfEmployed;
                existing.IsPassportVerified = profile.IsPassportVerified; 
                existing.IsEducationVerified = profile.IsEducationVerified;
                existing.IsModerationRequired = true;

                if (!string.IsNullOrWhiteSpace(profile.ImageUrl))
                    existing.ImageUrl = profile.ImageUrl;

                if (!string.IsNullOrWhiteSpace(profile.CertificateUrls))
                    existing.CertificateUrls = profile.CertificateUrls;
            }

            await _context.SaveChangesAsync();
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
                    ExperienceYears = p.ExperienceYears,
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
