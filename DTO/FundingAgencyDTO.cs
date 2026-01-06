using DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class FundingAgencyDTO
    {
        public int FundingAgencyID { get; set; }
        public string FundingAgencyName { get; set; } = string.Empty;
        public FundingAgencyType FundTypeID { get; set; } // 1 = Internal, 2 = External
    }

    public class AddFundingAgencyDTO
    {
        [Required(ErrorMessage = "Please select a Fund Type")]
        public FundingAgencyType FundTypeID { get; set; }

        [Required(ErrorMessage = "Please enter a Funding Agency Name")]
        public string FundingAgencyName { get; set; } = string.Empty;
    }

    public class UpdateFundingAgencyDTO
    {
        public int FundingAgencyID { get; set; }
        public string FundingAgencyName { get; set; } = string.Empty;
    }

}
