using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class ProjectParticipant
    {
        public int ProjectParticipantID { get; set; }

        public int ProjectID { get; set; }
        public int EmployeeID { get; set; }

        // Navigation
        public Project Project { get; set; }
        public Employee Employee { get; set; }
    }

}
