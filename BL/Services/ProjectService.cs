using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BL.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDBContext _context;

        public ProjectService(ApplicationDBContext context)
        {
           _context = context;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllProjectsAsync(string? search)
        {
            var query = _context.projects.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.ProjectName.Contains(search));
            }

            return await query.Select(p => new ProjectDTO
            {
                ProjectID = p.ProjectID,
                ProjectRefNumber = p.ProjectRefNumber,
                ProjectName = p.ProjectName,
                EmployeeID = p.EmployeeID,
                CoordinatorID = p.CoordinatorID,
                PillarLeadID = p.PillarLeadID,
                FundingAgencyID = p.FundingAgencyID,               
                Cost = p.Budget,               
                
                FundType = Convert.ToByte(p.FundType),

                AnnualOperationalObjective = p.AnnualOperationalObjective,
                SubStrategicObjective = p.SubStrategicObjective
            }).ToListAsync();
        }

        public async Task<ProjectDTO?> GetProjectInfoByProjectIDAsync(int ProjectID)
        {
            var project = await _context.projects
                .FirstOrDefaultAsync(p => p.ProjectID == ProjectID);

            if (project == null)
                return null;

            return new ProjectDTO
            {
                ProjectID = project.ProjectID,
                ProjectRefNumber = project.ProjectRefNumber,
                ProjectName = project.ProjectName,
                EmployeeID = project.EmployeeID,
                CoordinatorID = project.CoordinatorID,
                PillarLeadID = project.PillarLeadID,
                FundingAgencyID = project.FundingAgencyID,               
                Cost = project.Budget,
                FundType = Convert.ToByte(project.FundType),
                AnnualOperationalObjective = project.AnnualOperationalObjective,
                SubStrategicObjective = project.SubStrategicObjective,                
            };
        }

        public async Task<int> AddNewProjectAsync(AddNewProjectDTO dto)
        {
            if (dto.IsDraft)
                return await SaveProjectAsDraftAsync(dto);
            else
                return await CreateProjectAsync(dto);
        }

        private async Task<int> SaveProjectAsDraftAsync(AddNewProjectDTO dto)
        {
            var project = new Project
            {
                ProjectRefNumber = dto.ProjectRefNumber,
                ProjectName = dto.ProjectName,
                EmployeeID = dto.EmployeeID,
                CoordinatorID = dto.CoordinatorID,
                PillarLeadID = dto.PillarLeadID,
                FundingAgencyID = dto.FundingAgencyID,
                Currency = (Project.enCurrency)dto.CurrencyID,
                Budget = dto.Budget,
                FundType = (Project.enFundType)dto.FundType,
                AnnualOperationalObjective = dto.AnnualOperationalObjective,
                SubStrategicObjective = dto.SubStrategicObjective,
                FromDate = dto.FromDate,
                ToDate = dto.IsOngoing ? null : dto.ToDate,
                IsOngoing = dto.IsOngoing,
                IsDraft = true,
                CreatedDate = DateTime.Now
            };

            _context.projects.Add(project);
            await _context.SaveChangesAsync();

            // Save participants
           // await SaveParticipantsAsync(dto.ParticipantIDs, project.ProjectID);

            return project.ProjectID;
        }

        private async Task<int> CreateProjectAsync(AddNewProjectDTO dto)
        {
            var project = new Project
            {
                ProjectRefNumber = dto.ProjectRefNumber,
                ProjectName = dto.ProjectName,
                EmployeeID = dto.EmployeeID,
                CoordinatorID = dto.CoordinatorID,
                PillarLeadID = dto.PillarLeadID,
                FundingAgencyID = dto.FundingAgencyID,
                Currency = (Project.enCurrency)dto.CurrencyID,
                Budget = dto.Budget,
                FundType = (Project.enFundType)dto.FundType,
                AnnualOperationalObjective = dto.AnnualOperationalObjective,
                SubStrategicObjective = dto.SubStrategicObjective,
                FromDate = dto.FromDate,
                ToDate = dto.IsOngoing ? null : dto.ToDate,
                IsOngoing = dto.IsOngoing
            };

            _context.projects.Add(project);
            await _context.SaveChangesAsync(); // ⬅️ مهم للحصول على ProjectID

            // ✅ حفظ المشاركين
            if (dto.ParticipantIDs != null && dto.ParticipantIDs.Any())
            {
                var participants = dto.ParticipantIDs.Select(empId => new ProjectParticipant
                {
                    ProjectID = project.ProjectID,
                    EmployeeID = empId
                });

                _context.ProjectParticipants.AddRange(participants);
                await _context.SaveChangesAsync();
            }

            return project.ProjectID;
        }

        //public async Task<int> AddNewProjectAsync(AddNewProjectDTO dto)
        //{
        //    var project = new Project
        //    {
        //        ProjectRefNumber = dto.ProjectRefNumber,
        //        ProjectName = dto.ProjectName,
        //        EmployeeID = dto.EmployeeID,
        //        CoordinatorID = dto.CoordinatorID,
        //        PillarLeadID = dto.PillarLeadID,
        //        FundingAgencyID = dto.FundingAgencyID,          
        //        Currency = (Project.enCurrency)dto.CurrencyID,
        //        Budget = dto.Budget,


        //        // 👇 نحول byte القادم من الـ DTO إلى enum
        //        FundType = (Project.enFundType)dto.FundType,


        //        AnnualOperationalObjective = dto.AnnualOperationalObjective,
        //        SubStrategicObjective = dto.SubStrategicObjective
        //    };

        //    _context.projects.Add(project);
        //    return await _context.SaveChangesAsync();
        //}

        // Dropdowns
        public async Task<IEnumerable<EmployeeDropdownDTO>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDropdownDTO
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.EmployeeName
                }).ToListAsync();
        }

        public async Task<IEnumerable<EmployeeDropdownDTO>> GetAllParticipantsAsync() {
            return await _context.Employees
                    .Select(e => new EmployeeDropdownDTO
                    {
                        EmployeeID = e.EmployeeId,
                        EmployeeName = e.EmployeeName
                    }).ToListAsync(); 
        }

        public async Task<IEnumerable<EmployeeDropdownDTO>> GetAllCoordinatorsAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDropdownDTO
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.EmployeeName
                }).ToListAsync(); // لا ترجع null
        }

        public async Task<IEnumerable<EmployeeDropdownDTO>> GetAllPillarLeadsAsync()
        {
            return await _context.Employees
                .Select(e => new EmployeeDropdownDTO
                {
                    EmployeeID = e.EmployeeId,
                    EmployeeName = e.EmployeeName
                }).ToListAsync(); // لا ترجع null
        }

        public async Task<IEnumerable<FundingAgencyDropdownDTO>> GetFundingAgenciesByTypeAsync(byte fundTypeID)
        {
            return await _context.FundingAgency
                .Where(f => f.FundTypeID == (FundingAgencyType)fundTypeID) // cast byte to enum
                .Select(f => new FundingAgencyDropdownDTO
                {
                    FundTypeID = (byte)f.FundTypeID,// convert enum back to byte for DTO
                    FundingAgencyID = f.FundingAgencyID,
                    FundingAgencyName = f.FundingAgencyName
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<MainPillarDTO>> GetAllMainPillarsAsync() { 
            return await _context.MainPillars
                .Select(pillar => new MainPillarDTO { 
                    MainPillarID = pillar.MainPillarID,
                    MainPillarName = pillar.MainPillarName,
                    }).ToListAsync();
        }

        public async Task<IEnumerable<SubKpiDTO>> GetSubKpiByMainPillar(int mainPillarID)
        {
            return await _context.SubIndicators
                .Where(p => p.MainPillarID == mainPillarID)
                .Select(p => new SubKpiDTO
                {
                    SubIndicatorID = p.SubIndicatorID,
                    SubIndicatorName = p.SubIndicatorName,
                    MainPillarID = p.MainPillarID
                })
                .ToListAsync();
        }
               
        public async Task<IEnumerable<MeasurementMethodDTO>> GetMeasurementMethodsAsync()
        {
            return await _context.MeasurementMethods
                .Select(pillar => new MeasurementMethodDTO
                {
                    MeasurementMethodID = pillar.MeasurementMethodID,
                    MeasurementMethod = pillar.MeasurementMethodName,
                }).ToListAsync();
        }
               

        /* public async Task<IEnumerable<MeasurementIndicatorsDTO>> GetMeasurementIndicatorsBySubIndicatorAsync(int subIndicatorID)
         {
             return await _context.MeasurementIndicators
                 .Where(indicator => indicator.SubIndicatorID == subIndicatorID)
                 .Select(indicator => new MeasurementIndicatorsDTO
                 {
                     Measurement_IndicatorID = indicator.MeasurementIndicatorID,
                     MeasurementIndicator = indicator.MeasurementIndicatorName,
                     SubIndicatorID = indicator.SubIndicatorID,                  
                 })
                 .ToListAsync();
         }*/
    }
}
