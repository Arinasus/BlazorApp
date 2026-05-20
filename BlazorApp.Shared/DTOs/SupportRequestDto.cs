using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.DTOs
{
    public class SupportRequestDto
    {
        [Required(ErrorMessage = "Введите ваше имя")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите Email для связи")]
        [EmailAddress(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Выберите тему обращения")]
        public string Subject { get; set; } = "Техническая проблема";

        [Required(ErrorMessage = "Опишите вашу проблему")]
        [StringLength(1000, ErrorMessage = "Сообщение слишком длинное")]
        public string Message { get; set; } = string.Empty;
    }
}
