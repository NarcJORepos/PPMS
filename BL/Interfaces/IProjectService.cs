using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models.Entities;
using DTO;


namespace BL.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync(string? search);
        Task<int> AddNewProjectAsync(AddNewProjectDTO dto);
       
        
        //Task<ProjectDTO> GetProjectInfoByProjectIDAsync(int ProjectID);
        

        // للقوائم dropdown
        Task<IEnumerable<EmployeeDropdownDTO>> GetAllEmployeesAsync();
        Task<IEnumerable<EmployeeDropdownDTO>> GetAllParticipantsAsync();
        Task<IEnumerable<EmployeeDropdownDTO>> GetAllCoordinatorsAsync();
        Task<IEnumerable<EmployeeDropdownDTO>> GetAllPillarLeadsAsync();
        Task<IEnumerable<FundingAgencyDropdownDTO>> GetFundingAgenciesByTypeAsync(byte fundType);

        Task<IEnumerable<MainPillarDTO>> GetAllMainPillarsAsync();
        Task<IEnumerable<SubKpiDTO>> GetSubKpiByMainPillar(int MainPillarID);
        Task<IEnumerable<MeasurementMethodDTO>> GetMeasurementMethodsAsync();        
       

        //Task<IEnumerable<MeasurementIndicatorsDTO>> GetMeasurementIndicatorsBySubIndicatorAsync(int SubIndicatorID);

    }
}

