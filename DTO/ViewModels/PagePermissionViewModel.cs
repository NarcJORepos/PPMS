using System.Collections.Generic;

namespace PPMS.ViewModels
{
    public class PagePermissionViewModel
    {
        public int RoleID { get; set; }       // <-- أضف هذا
        public int GroupID { get; set; }      // <-- وأضف هذا
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
