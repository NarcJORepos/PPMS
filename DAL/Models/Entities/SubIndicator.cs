using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class SubIndicator
    {
        public int SubIndicatorID { get; set; }
        public string SubIndicatorName { get; set; } = string.Empty;

        public int MainPillarID { get; set; }       
       
    }
}
