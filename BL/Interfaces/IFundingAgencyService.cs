using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BL.Interfaces
{
    public interface IFundingAgencyService
    {
       Task<IEnumerable<FundingAgencyDTO>> GetAllFundingAgenciesAsync();
       Task<int> AddNewFundingAgencyAsync(AddFundingAgencyDTO agencyDTO);

    }
}
