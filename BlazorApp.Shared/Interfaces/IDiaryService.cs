using BlazorApp.Shared.DTOs;
using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface IDiaryService
    {
        Task<List<TherapyDiaryLogDto>> GetLogsForTherapistAsync(int therapistId);
        Task<List<TherapyDiaryLogDto>> GetLogsForParentAsync(string parentId);
        Task<bool> AddDiaryLogAsync(TherapyDiaryLogDto dto);

        Task<bool> SendInvitationAsync(int therapistId, string parentEmail);
        Task<List<DiaryInvitation>> GetPendingInvitationsAsync(string parentEmail);
        Task<bool> AcceptInvitationAsync(int invitationId, int childId, string parentId);
        Task<List<Child>> GetActiveChildrenForTherapistAsync(int therapistId);
        Task<List<ChildConnectionDto>> GetConnectedChildrenForTherapistAsync(int therapistId);
        Task<List<InvitationStatusDto>> GetInvitationsByTherapistAsync(int therapistId);
    }
}
