using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ViewModels
{
    public class RolePermissionViewModel
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public int GroupID { get; set; }

        public List<PagePermissionItem> Pages { get; set; } = new();
    }

    public class PagePermissionItem
    {
        public int PageID { get; set; }
        public string PageName { get; set; } = string.Empty;

        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }
    }

}
