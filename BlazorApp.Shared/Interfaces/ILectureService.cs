using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface ILectureService
    {
        Task<List<Lecture>> GetAllLecturesAsync();
        Task<List<Lecture>> GetLecturesByDiagnosisAsync(string diagnosis);
        Task<List<Lecture>> GetLecturesByAuthorAsync(string authorId);
        Task AddLectureAsync(Lecture lecture);
        Task UpdateLectureAsync(Lecture lecture);
        Task DeleteLectureAsync(int id);
    }
}
