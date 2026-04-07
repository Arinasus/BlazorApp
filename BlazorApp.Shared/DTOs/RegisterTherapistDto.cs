using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.DTOs
{
    public class RegisterTherapistDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = "";
        [Required, MinLength(6)]
        public string Password { get; set; } = "";
        [Required]
        public string Specialization { get; set; } = "";
        [Range(0,50)]
        public int ExperienceYears { get; set;  }
    }
}
