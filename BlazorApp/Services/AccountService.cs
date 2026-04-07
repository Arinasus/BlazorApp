using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using BlazorApp.Shared.DTOs;
using BlazorApp.Shared.Models;
using System.Diagnostics.CodeAnalysis;
namespace BlazorApp.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IdentityResult> RegisterTherapistAsync(RegisterTherapistDto dto)
        {
            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Therapist");

                var profile = new TherapistProfile
                {
                    UserId = user.Id,
                    Specialization = dto.Specialization,
                    ExperienceYears = dto.ExperienceYears
                };
                _context.TherapistProfiles.Add(profile);
                await _context.SaveChangesAsync();
            }
            return result;
        }
    }
}
