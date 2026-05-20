using BlazorApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface IFavoriteService
    {
        Task ToggleFavoriteAsync(string userId, int? lectureId = null, int? therapistId = null);
        Task<List<Favorite>> GetUserFavoritesAsync(string userId);
        Task<bool> IsFavoriteAsync(string userId, int? lectureId = null, int? therapistId = null);
    }
}
