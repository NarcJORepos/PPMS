using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class MainPillar
    {
        public int MainPillarID { get; set; }
        public string MainPillarName { get; set; } = string.Empty;

        //public ICollection<SubIndicator> SubIndicators { get; set; }
    }
}
