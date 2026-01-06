using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DTO;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using DAL.Models.Entities;

namespace BL.Services
{
    public class FundingAgencyService : IFundingAgencyService
    {
        public readonly ApplicationDBContext _dbContext;
        public FundingAgencyService(ApplicationDBContext context)
        {
            _dbContext = context;
        }
        public async Task<int> AddNewFundingAgencyAsync(AddFundingAgencyDTO agencyDTO)
        {           
            var newAgency = new FundingAgency
            {
                FundingAgencyName = agencyDTO.FundingAgencyName,
            };

            _dbContext.FundingAgency.Add(newAgency);

            await _dbContext.SaveChangesAsync();

            return newAgency.FundingAgencyID;
        }

        public async Task<IEnumerable<FundingAgencyDTO>> GetAllFundingAgenciesAsync()
        {
            return await _dbContext.FundingAgency
                .Select(agency => new FundingAgencyDTO
                {
                    FundingAgencyID = agency.FundingAgencyID,
                    FundingAgencyName = agency.FundingAgencyName,   
                }).ToListAsync();                
        }        
    }
}
