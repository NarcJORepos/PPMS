using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{    
        // DTO for displaying operational objectives
    public class OperationalObjectiveDTO
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
        public decimal? AchievementPercent { get; set; }
        public int ProjectID { get; set; }

        public List<string> Objectives { get; set; } = new List<string>();
    }

        // DTO for adding a new operational objective
    public class AddOperationalObjectiveDTO
    {
        public string OperationalObjectiveName { get; set; } = string.Empty;
        public string Baseline_Value { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ResponsiblePerson { get; set; }
        public string? KPI { get; set; }
        public string? QualitativeOutput { get; set; }
        public string? QuantitativeOutput { get; set; }
        public decimal? AchievementPercent { get; set; }
        
        [Required(ErrorMessage = "Please select a project")]
        public int ProjectID { get; set; }

        public List<string> Objectives { get; set; } = new List<string>();
    }

    // Lightweight DTO for listing operational objectives
    public class OperationalObjectiveListDTO
    {
        public int OperationalObjectiveID { get; set; }
        public string OperationalObjectiveName { get; set; } = string.Empty;
        public int ProjectID { get; set; }
    }

    //dropdowns

    public class ProjectDropdownDTO
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; } = string.Empty;
    }

}

