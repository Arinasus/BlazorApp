using BlazorApp.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Shared.Interfaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersAsync();
        Task UpdateRoleAsync(string userId, string newRole);
        Task UpdateBlockStatusAsync(string userId, bool isBlocked);
        Task DeleteUserAsync(string userId);
    }
}
