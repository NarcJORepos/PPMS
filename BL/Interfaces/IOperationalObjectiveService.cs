using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
namespace BL.Interfaces
{
    public interface IOperationalObjectiveService
    {        
        Task<int> AddNewOperationalObjectiveAsync(AddOperationalObjectiveDTO dto);
        Task<IEnumerable<OperationalObjectiveListDTO>> GetAllOperationalObjectivesAsync(int projectID);             
        Task<OperationalObjectiveDTO?> GetOperationalObjectiveByIdAsync(int operationalObjectiveID);
        Task<IEnumerable<ProjectDropdownDTO>> GetProjectsForDropdownAsync();

        Task UpdateOperationalObjectiveAsync(int id, AddOperationalObjectiveDTO dto);
    }
}
