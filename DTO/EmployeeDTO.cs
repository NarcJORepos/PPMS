using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class EmployeeDTO
    {       
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        
        public int GroupID { get; set; }
        public string GroupName { get; set; } = string.Empty;

        public int DirectorateID { get; set; }
        public string DirectorateName { get; set; } = string.Empty; 

        public enum enGendor { Male = 0, Female = 1 }
        public enGendor Gendor { get; set; }
    }
}
