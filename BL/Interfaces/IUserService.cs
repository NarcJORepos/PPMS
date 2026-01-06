using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;

namespace BL.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync(string? search = null);
        Task<UserDTO?> GetUserInfoByIdAsync(int id);
        Task<int> CreateUserAsync(CreateUserDTO dto);
        Task<int> UpdateUserAsync(UserDTO dto);
        Task<int> DeleteUserAsync(int id);
    }
}

