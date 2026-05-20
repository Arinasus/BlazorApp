using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int? LectureId { get; set; }
        public Lecture? Lecture { get; set; }
        public int? TherapistProfileId {  get; set; }
        public TherapistProfile? TherapistProfile { get; set; }
    }
}
