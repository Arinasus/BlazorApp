using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? ImagePath { get; set; }
        public string VideoUrl { get; set; } = string.Empty;
        public string TargetDiagnosis { get; set; } = string.Empty;
    }
}
