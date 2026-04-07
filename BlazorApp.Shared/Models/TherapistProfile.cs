using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class TherapistProfile
    {
        public int Id { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string Education {  get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal PricePerHour { get; set; }
       // Moderate Status
        public bool IsApproved { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public string UserId { get; set; } = string.Empty;
    }
}
