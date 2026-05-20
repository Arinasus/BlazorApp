using BlazorApp.Data;
using BlazorApp.Shared.DTOs;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public DiaryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        #region Методы Дневника (Logs)

        public async Task<List<TherapyDiaryLogDto>> GetLogsForTherapistAsync(int therapistId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            return await context.TherapyDiaryLogs
                .Where(l => l.TherapistProfileId == therapistId)
                .OrderByDescending(l => l.SessionDate)
                .Select(log => new TherapyDiaryLogDto
                {
                    Id = log.Id,
                    TherapistProfileId = log.TherapistProfileId,
                    ChildName = log.ChildName,
                    ParentId = log.ParentId,
                    SessionDate = log.SessionDate,
                    Topic = log.Topic,
                    WorkDone = log.WorkDone,
                    Homework = log.Homework,
                    Notes = log.Notes
                })
                .ToListAsync();
        }

        public async Task<List<TherapyDiaryLogDto>> GetLogsForParentAsync(string parentId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var activeChildIds = await context.DiaryInvitations
                .Where(i => i.ParentId == parentId && i.Status == "Accepted" && i.TargetChildId.HasValue)
                .Select(i => i.TargetChildId!.Value)
                .ToListAsync();

            return await context.TherapyDiaryLogs
                .Where(l => l.TargetChildId.HasValue && activeChildIds.Contains(l.TargetChildId.Value))
                .OrderByDescending(l => l.SessionDate)
                .Select(log => new TherapyDiaryLogDto
                {
                    Id = log.Id,
                    TherapistProfileId = log.TherapistProfileId,
                    ChildName = log.ChildName,
                    ParentId = log.ParentId,
                    SessionDate = log.SessionDate,
                    Topic = log.Topic,
                    WorkDone = log.WorkDone,
                    Homework = log.Homework,
                    Notes = log.Notes
                })
                .ToListAsync();
        }

        public async Task<bool> AddDiaryLogAsync(TherapyDiaryLogDto dto)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var child = await context.Children.FindAsync(dto.TargetChildId);
            if (child == null) return false;

            // Исправление: Если дата пришла из формы как Local или Unspecified, 
            // мы принудительно говорим PostgreSQL, что это UTC, чтобы он не ругался
            var sessionDateUtc = dto.SessionDate.Kind == DateTimeKind.Utc
                ? dto.SessionDate
                : DateTime.SpecifyKind(dto.SessionDate, DateTimeKind.Utc);

            var entity = new TherapyDiaryLog
            {
                TherapistProfileId = dto.TherapistProfileId,
                TargetChildId = dto.TargetChildId,
                ChildName = child.Name,
                ParentId = child.ParentId,
                SessionDate = sessionDateUtc, // <-- Передаем исправленную дату
                Topic = dto.Topic,
                WorkDone = dto.WorkDone,
                Homework = dto.Homework,
                Notes = dto.Notes,
                CreatedAt = DateTime.UtcNow // Здесь у тебя уже было Utc, тут всё супер!
            };

            await context.TherapyDiaryLogs.AddAsync(entity);
            return await context.SaveChangesAsync() > 0;
        }

        #endregion

        #region Методы Приглашений (Invitations)

        public async Task<bool> SendInvitationAsync(int therapistId, string parentEmail)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var exists = await context.DiaryInvitations
                .AnyAsync(i => i.TherapistProfileId == therapistId && i.ParentEmail.ToLower() == parentEmail.ToLower() && i.Status != "Rejected");

            if (exists) return false;

            var invitation = new DiaryInvitation
            {
                TherapistProfileId = therapistId,
                ParentEmail = parentEmail.Trim(),
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            await context.DiaryInvitations.AddAsync(invitation);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<DiaryInvitation>> GetPendingInvitationsAsync(string parentEmail)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            return await context.DiaryInvitations
                .Where(i => i.ParentEmail.ToLower() == parentEmail.ToLower() && i.Status == "Pending")
                .ToListAsync();
        }

        public async Task<bool> AcceptInvitationAsync(int invitationId, int childId, string parentId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var invitation = await context.DiaryInvitations.FindAsync(invitationId);
            if (invitation == null) return false;

            invitation.Status = "Accepted";
            invitation.TargetChildId = childId;
            invitation.ParentId = parentId;

            context.DiaryInvitations.Update(invitation);
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<List<Child>> GetActiveChildrenForTherapistAsync(int therapistId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var childIds = await context.DiaryInvitations
                .Where(i => i.TherapistProfileId == therapistId && i.Status == "Accepted" && i.TargetChildId.HasValue)
                .Select(i => i.TargetChildId!.Value)
                .ToListAsync();

            return await context.Children
                .Where(c => childIds.Contains(c.Id))
                .ToListAsync();
        }
        public async Task<List<ChildConnectionDto>> GetConnectedChildrenForTherapistAsync(int therapistId)
        {
            // 1. Создаем контекст через фабрику, как и в других твоих методах
            using var context = await _contextFactory.CreateDbContextAsync();

            // 2. Делаем Join между принятыми инвайтами и таблицей детей, чтобы узнать имя ребенка
            return await context.DiaryInvitations
                .Where(i => i.TherapistProfileId == therapistId && i.Status == "Accepted" && i.TargetChildId.HasValue)
                .Join(context.Children,
                    invitation => invitation.TargetChildId,
                    child => child.Id,
                    (invitation, child) => new ChildConnectionDto
                    {
                        ChildId = child.Id,
                        ChildName = child.Name,       
                        ParentEmail = invitation.ParentEmail
                    })
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<InvitationStatusDto>> GetInvitationsByTherapistAsync(int therapistId)
        {
            // 1. Создаем контекст через фабрику
            using var context = await _contextFactory.CreateDbContextAsync();

            // 2. Маппим данные из реальной таблицы DiaryInvitations
            return await context.DiaryInvitations
                .Where(i => i.TherapistProfileId == therapistId)
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new InvitationStatusDto
                {
                    ParentEmail = i.ParentEmail,
                    CreatedAt = i.CreatedAt,
                    IsAccepted = i.Status == "Accepted"
                })
                .ToListAsync();
        }
        
        #endregion
    }
}