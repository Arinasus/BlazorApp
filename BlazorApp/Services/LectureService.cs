using BlazorApp.Data;
using BlazorApp.Shared.Interfaces;
using BlazorApp.Shared.Models;
using Microsoft.EntityFrameworkCore;
namespace BlazorApp.Services
{
    public class LectureService : ILectureService
    {
        private readonly ApplicationDbContext _context;

        public LectureService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. Получить вообще все лекции (для Админа)
        public async Task<List<Lecture>> GetAllLecturesAsync()
            => await _context.Lectures.ToListAsync();

        // 2. Поиск лекций по диагнозу (для ленты пользователей)
        public async Task<List<Lecture>> GetLecturesByDiagnosisAsync(string diagnosis)
            => await _context.Lectures
                .Where(l => l.TargetDiagnosis.Contains(diagnosis))
                .ToListAsync();

        // 3. Получить лекции конкретного автора (для личного кабинета специалиста)
        public async Task<List<Lecture>> GetLecturesByAuthorAsync(string authorId)
            => await _context.Lectures
                .Where(l => l.AuthorId == authorId)
                .ToListAsync();

        // 4. Добавить новую лекцию
        public async Task AddLectureAsync(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            await _context.SaveChangesAsync();
        }

        // 5. Редактировать существующую лекцию
        public async Task UpdateLectureAsync(Lecture lecture)
        {
            var existing = await _context.Lectures.FindAsync(lecture.Id);
            if (existing != null)
            {
                // Обновляем только текстовые поля, сохраняя структуру
                _context.Entry(existing).CurrentValues.SetValues(lecture);
                await _context.SaveChangesAsync();
            }
        }

        // 6. Удалить лекцию из системы
        public async Task DeleteLectureAsync(int id)
        {
            var lecture = await _context.Lectures.FindAsync(id);
            if (lecture != null)
            {
                _context.Lectures.Remove(lecture);
                await _context.SaveChangesAsync();
            }
        }
    }
}
