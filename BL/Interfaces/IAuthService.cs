using DTO;
using DAL.Models.Entities;

namespace BL.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult?> AuthenticateAsync(LoginUserDTO loginDto);
    }

    public class AuthResult
    {
        public bool IsSuccess { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int RoleID { get; set; }
    }
}
