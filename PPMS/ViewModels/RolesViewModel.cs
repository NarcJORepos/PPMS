using DTO;

namespace PPMS.ViewModels
{
    public class RolesViewModel
    {
        public int SelectedGroupID { get; set; } // Group المختار
        public IEnumerable<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
    }

}
