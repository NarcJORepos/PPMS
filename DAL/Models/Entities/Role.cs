    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class Role
    {
        public int ID { get; set; } // Primary Key في قاعدة البيانات
        public int PageID { get; set; }
        public int GroupID { get; set; }

        public bool CanAdd { get; set; }
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public bool CanView { get; set; }

        // العلاقات
        public virtual Page Page { get; set; } = null!;
        public virtual Group Group { get; set; } = null!;
    }
}
