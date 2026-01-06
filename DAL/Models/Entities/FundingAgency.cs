using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.Entities
{
    public class FundingAgency
    {
        public int FundingAgencyID { get; set; }
        public string FundingAgencyName { get; set; } = string.Empty;

        public FundingAgencyType FundTypeID { get; set; } // 1 = Internal, 2 = External

    }
}
