using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LoginUserDTO
    {
        [Required(ErrorMessage ="Please Enter your Username")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please Enter your Password")]       
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
