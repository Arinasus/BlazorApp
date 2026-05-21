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

        public async Task<List<Lecture>> GetAllLecturesAsync()
            => await _context.Lectures.ToListAsync();

        public async Task<List<Lecture>> GetLecturesByDiagnosisAsync(string diagnosis)
            => await _context.Lectures
                .Where(l => l.TargetDiagnosis.Contains(diagnosis))
                .ToListAsync();

        public async Task<List<Lecture>> GetLecturesByAuthorAsync(string authorId)
            => await _context.Lectures
                .Where(l => l.AuthorId == authorId)
                .ToListAsync();

        public async Task AddLectureAsync(Lecture lecture)
        {
            _context.Lectures.Add(lecture);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLectureAsync(Lecture lecture)
        {
            var existing = await _context.Lectures.FindAsync(lecture.Id);
            if (existing != null)
            {
                existing.Title = lecture.Title;
                existing.TargetDiagnosis = lecture.TargetDiagnosis;
                existing.Description = lecture.Description;
                existing.Content = lecture.Content;
                existing.VideoUrl = lecture.VideoUrl;

                if (!string.IsNullOrEmpty(lecture.ImagePath))
                {
                    existing.ImagePath = lecture.ImagePath;
                }

                await _context.SaveChangesAsync();
            }
        }

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
