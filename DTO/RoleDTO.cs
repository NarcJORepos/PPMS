using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class RoleDTO
    {
        public int ID { get; set; }       
        public int GroupID { get; set; }
        public string GroupName { get; set; } = string.Empty;

        public int PageID { get; set; }
        public string PageName { get; set; } = string.Empty;

        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }

    }
}
