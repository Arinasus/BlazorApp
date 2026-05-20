using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Models
{
    public class TherapyDiaryLog
    {
        public int Id { get; set; }
        public int TherapistProfileId { get; set; } // К какому логопеду привязано
        public int? TargetChildId { get; set; }
        public string ChildName { get; set; } = string.Empty; // Имя ребенка
        public string ParentId { get; set; } = string.Empty; // Ссылка на родителя
        public DateTime SessionDate { get; set; } = DateTime.Today; // Дата занятия
        public string Topic { get; set; } = string.Empty; // Тема занятия (например, "Постановка звука [Р]")
        public string WorkDone { get; set; } = string.Empty; // Что было сделано
        public string Homework { get; set; } = string.Empty; // Домашнее задание
        public string Notes { get; set; } = string.Empty; // Заметки логопеда о прогрессе
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
