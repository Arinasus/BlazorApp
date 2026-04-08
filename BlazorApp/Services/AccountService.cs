using BlazorApp.Data;
using Microsoft.AspNetCore.Identity;
using BlazorApp.Shared.DTOs;
using BlazorApp.Shared.Models;
using System.Diagnostics.CodeAnalysis;
namespace BlazorApp.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public AccountService(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
    }
}
