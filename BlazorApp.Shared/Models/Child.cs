using Fluent.Infrastructure.FluentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class Child
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string ParentId { get; set; } = string.Empty;
        [ForeignKey("ParentId")]
        public virtual ApplicationUser? Parent {  get; set; }
        [Required(ErrorMessage = "Введите имя ребенка")]
        public string Name { get; set; } = string.Empty;
        [Range(0, 18, ErrorMessage = "Возраст должен быть от 0 до 18 лет")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Укажите диагноз или особенности развития")]
        public string Diagnosis { get; set; } = string.Empty;

        public string? Notes { get; set; } 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
