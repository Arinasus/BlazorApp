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
        Task<List<TherapistProfile>> GetPendingApplicationsAsync();
    }
}
