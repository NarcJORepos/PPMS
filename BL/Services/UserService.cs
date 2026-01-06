using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUserAsync(CreateUserDTO dto)
        {
            bool usernameExists = await _context.Users
                .AnyAsync(u => u.Username.ToLower() == dto.Username.ToLower());

            if (usernameExists)
                return -1;
            

            var user = new User
            {
                EmployeeID = dto.EmployeeID,
                Username = dto.Username,
                Password = dto.Password, // لاحقاً يمكن تشفيره قبل الحفظ
                IsActive = dto.IsActive
            };

            _context.Users.Add(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateUserAsync(UserDTO dto)
        {
            var user = await _context.Users.FindAsync(dto.ID);
            if (user == null) return 0;

            user.EmployeeID = dto.EmployeeID;
            user.Username = dto.Username;
            user.IsActive = dto.IsActive;

            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return 0;

            _context.Users.Remove(user);

            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? search = null)
        {
            var query = _context.Users.Include(u => u.Employee).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Username.Contains(search) ||
                                         (u.Employee != null && u.Employee.EmployeeName.Contains(search)));
            }

            return await query.Select(
                user => new UserDTO
                {
                    ID = user.ID,
                    EmployeeID = user.EmployeeID,
                    EmployeeName = user.Employee != null ? user.Employee.EmployeeName : string.Empty,
                    Username = user.Username,
                    IsActive = user.IsActive
                }).ToListAsync();
        }

        public async Task<UserDTO?> GetUserInfoByIdAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Employee).FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return null;

            return new UserDTO
            {
                ID = user.ID,
                EmployeeID = user.EmployeeID,
                Username = user.Username,
                IsActive = user.IsActive
            };
        }
    }
}
