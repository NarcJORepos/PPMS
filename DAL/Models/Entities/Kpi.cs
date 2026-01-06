using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class Kpi
    {
        public int KpiID { get; set; }
        public string KpiName { get; set; } = string.Empty;        
        public string? KpiUnit { get; set; }

        public int SubIndicatorID { get; set; }
        public int MeasurementMethodID { get; set; }   
        public string MeasurementFrequency { get; set; } = string.Empty;
               

       // public ICollection<ProjectKpi> ProjectKpis { get; set; }
    }

}
