using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class TherapistReview
    {
        public int Id { get; set; }
        public int TherapistProfileId { get; set; }
        public string AuthorName { get; set; } = string.Empty;
        public string Content {  get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public TherapistProfile? TherapistProfile { get; set; }
    }
}
