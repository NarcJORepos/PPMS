using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public enum enGendor { Male = 0, Female = 1 };
        public enGendor Gendor { get; set; }

        //------------- FK-------------
        public int DirectorateId { get; set; }
        public virtual Directorate? Directorate { get; set; }

        public int GroupId { get; set; }  
        public virtual Group? Group { get; set; }

    }
}
