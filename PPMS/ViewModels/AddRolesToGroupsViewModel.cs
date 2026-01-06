using DTO;
using System.Collections.Generic;

namespace PPMS.ViewModels
{
    public class AddRolesToGroupsViewModel
    {
        public int GroupID { get; set; }
        public List<RoleDTO> Roles { get; set; } = new();
    }
}
