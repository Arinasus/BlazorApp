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
        [Required(ErrorMessage = "Укажите специализацию")]
        public string Specialization { get; set; } = string.Empty;

        [Required(ErrorMessage = "Опишите ваше образование")]
        public string Education { get; set; } = string.Empty;

        [Range(0, 50, ErrorMessage = "Стаж должен быть от 0 до 50 лет")]
        public int ExperienceYears { get; set; }

        [Range(1, 100000, ErrorMessage = "Укажите корректную цену")]
        public decimal PricePerHour { get; set; }
    }
}
