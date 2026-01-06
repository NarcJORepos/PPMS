using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Entities;
using DTO;

namespace BL.Interfaces
{
    public interface IDirectorateService
    {
      Task<IEnumerable<DirectorateDTO>> ViewAllIDirectoratesAsync();

     Task<int> AddNewDirectorateAsync(DirectorateDTO directorateDTO);     
    }

}
