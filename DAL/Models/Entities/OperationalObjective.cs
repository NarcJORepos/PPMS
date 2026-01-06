using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class OperationalObjective
    {
        public int OperationalObjectiveID { get; set; }
        public string OperationalObjectiveName { get; set; } = string.Empty;
        public string Baseline_Value { get; set; } = string.Empty;  
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ResponsiblePerson { get; set; }
        public string? KPI { get; set; }       
        public string? QualitativeOutput { get; set; }
        public string? QuantitativeOutput { get; set; }       
        public decimal? AchievementPercent { get; set; } //(5, 2)
        public int ProjectID { get; set; }
    }
}

	
   