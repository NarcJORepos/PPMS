using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Data;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class DirectorateService : IDirectorateService
    {
        private readonly ApplicationDBContext _dbContext;
        public DirectorateService(ApplicationDBContext dbContext)
        {
            this._dbContext = dbContext;
        }
      
        public async Task<int> AddNewDirectorateAsync(DirectorateDTO directorateDTO)
        {
            var newDirectorate = new DAL.Models.Entities.Directorate
            {
                DirectorateName = directorateDTO.DirectorateName,
            };

            _dbContext.directorates.Add(newDirectorate);

            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DirectorateDTO>> ViewAllIDirectoratesAsync()
        {
            return await _dbContext.directorates
            .OrderBy(directorate => directorate.DirectorateName)
            .Select(directorate => new DirectorateDTO
            {
                DirectorateID = directorate.DirectorateID,
                DirectorateName = directorate.DirectorateName,
            }).ToListAsync();
        }
    }
}
