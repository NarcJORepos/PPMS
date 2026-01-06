using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class AddRolesToGroupViewModel
    {
        public int GroupID { get; set; }
        public List<RoleDTO> Roles { get; set; } = new();
    }
}
