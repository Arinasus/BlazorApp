using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class TherapistProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string Specialization { get; set; } = string.Empty;
        public string Education {  get; set; } = string.Empty;
        public int ExperienceYears { get; set; }
        public decimal PricePerHour { get; set; }
       // Moderate Status
        public bool IsApproved { get; set; } = false;
        public bool IsModerationRequired { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        
        public string? ImageUrl { get; set; }
        public string ShortDescription { get; set; } = string.Empty;

        public string FullBio { get; set; } = string.Empty;

        public string WorkFormat { get; set; } = "Online";
        public List<TherapistReview> Reviews { get; set; } = new();

        [NotMapped] 
        public double AverageRating
        {
            get
            {
                if (Reviews == null || !Reviews.Any()) return 0.0;
                return Math.Round(Reviews.Average(r => r.Rating), 1);
            }
        }
        public string? CertificateUrls { get; set; }
        //Contact Info
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string? Address { get; set; }
        public bool IsSelfEmployed { get; set; }
        public bool IsPassportVerified { get; set; }
        public bool IsEducationVerified { get; set; }

        public string SpeechDisorders { get; set; } = string.Empty; 
        public string SpecialNeeds { get; set; } = string.Empty;
        public string WorkType { get; set; } = "Индивидуальные";
    }
}
