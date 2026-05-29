using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface ITherapistService
    {
        Task CreateApplicationAsync(TherapistProfile profile);
        Task ApproveTherapistAsync(int profileId);
        Task UpdateVerificationStatusAsync(int profileId, bool isPassport, bool isEducation, bool isSelfEmployed);
        Task<List<TherapistProfile>> GetPendingApplicationsAsync();
        Task<TherapistProfile?> GetProfileByUserId(string userId);
        Task UpdateProfile(TherapistProfile profile);
        Task<List<TherapistProfile>> GetApprovedProfilesAsync();
        Task<List<TherapistReview>> GetReviewsByTherapistIdAsync(int therapistProfileId);
        Task<bool> AddReviewAsync(TherapistReview review);
    }
}
