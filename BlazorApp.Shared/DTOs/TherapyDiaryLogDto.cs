using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.DTOs
{
    public class TherapyDiaryLogDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        public int TherapistProfileId { get; set; }

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(100, ErrorMessage = "MaxLengthExceeded")]
        public string ChildName { get; set; } = string.Empty;

        public string ParentId { get; set; } = string.Empty;

        [Required(ErrorMessage = "RequiredField")]
        public DateTime SessionDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "RequiredField")]
        [StringLength(200, ErrorMessage = "MaxLengthExceeded")]
        public string Topic { get; set; } = string.Empty;

        [Required(ErrorMessage = "RequiredField")]
        public string WorkDone { get; set; } = string.Empty;

        public string Homework { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public int? TargetChildId { get; set; }
    }
}
