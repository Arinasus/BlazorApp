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
        Task AddLectureAsync(Lecture lecture);
    }
}
