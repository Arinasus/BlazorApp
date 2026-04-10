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
    }
}
