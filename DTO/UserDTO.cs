using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class UserDTO
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }

        public string EmployeeName { get; set; } = string.Empty;
       
        public string Username { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }

    public class CreateUserDTO
    {
        [Required]
        public int EmployeeID { get; set; }

        [Required, StringLength(20)]
        public string Username { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), StringLength(20, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
    }
}
