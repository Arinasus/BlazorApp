using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class DiaryInvitation
    {
        public int Id { get; set; }
        public int TherapistProfileId { get; set; } // Кто приглашает
        public string? ParentId { get; set; }
        public string ParentEmail { get; set; } = string.Empty; // Кого приглашают
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Rejected
        public int? TargetChildId { get; set; } // Заполнится, когда родитель примет инвайт
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
