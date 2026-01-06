using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.AspNetCore.Identity;
using Helpers;

namespace BL.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDBContext _context;

        public AuthService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<AuthResult?> AuthenticateAsync(LoginUserDTO loginDto)
        {
            if (loginDto == null || string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password)) return null;

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.IsActive);

            if (user == null) return null;

            if (!PasswordHelper.VerifyPassword(user.Password, loginDto.Password)) return null;

            return new AuthResult
            {
                IsSuccess = true,
                UserId = user.ID,
                Username = user.Username
            };
        }
    }
}
