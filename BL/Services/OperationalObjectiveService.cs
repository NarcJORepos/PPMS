using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Interfaces;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.EntityFrameworkCore;

namespace BL.Services
{
    public class OperationalObjectiveService : IOperationalObjectiveService
    {
        private readonly ApplicationDBContext _context;

        public OperationalObjectiveService(ApplicationDBContext context)
        {
            _context = context;
        }

        // Add New Operational Objective
        public async Task<int> AddNewOperationalObjectiveAsync(AddOperationalObjectiveDTO dto)
        {
            var entity = new OperationalObjective
            {
                OperationalObjectiveName = dto.OperationalObjectiveName,
                Baseline_Value = dto.Baseline_Value,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                ResponsiblePerson = dto.ResponsiblePerson,
                KPI = dto.KPI,
                QualitativeOutput = dto.QualitativeOutput,
                QuantitativeOutput = dto.QuantitativeOutput,
                AchievementPercent = dto.AchievementPercent,
                ProjectID = dto.ProjectID
            };

            _context.OperationalObjectives.Add(entity);
            await _context.SaveChangesAsync();

            return entity.OperationalObjectiveID;
        }

        // Get All Operational Objectives for a Project (Lightweight List)
        public async Task<IEnumerable<OperationalObjectiveListDTO>> GetAllOperationalObjectivesAsync(int projectID)
        {
            return await _context.OperationalObjectives
                .Where(o => o.ProjectID == projectID)
                .Select(o => new OperationalObjectiveListDTO
                {
                    OperationalObjectiveID = o.OperationalObjectiveID,
                    OperationalObjectiveName = o.OperationalObjectiveName,
                    ProjectID = o.ProjectID
                })
                .ToListAsync();
        }

        // Get Operational Objective Details by ID (Full DTO)
        public async Task<OperationalObjectiveDTO?> GetOperationalObjectiveByIdAsync(int OperationalObjectiveID)
        {
            return await _context.OperationalObjectives
                .Where(o => o.OperationalObjectiveID == OperationalObjectiveID)
                .Select(o => new OperationalObjectiveDTO
                {
                    OperationalObjectiveID = o.OperationalObjectiveID,
                    OperationalObjectiveName = o.OperationalObjectiveName,
                    Baseline_Value = o.Baseline_Value,
                    StartDate = o.StartDate,
                    EndDate = o.EndDate,
                    ResponsiblePerson = o.ResponsiblePerson,
                    KPI = o.KPI,
                    QualitativeOutput = o.QualitativeOutput,
                    QuantitativeOutput = o.QuantitativeOutput,
                    AchievementPercent = o.AchievementPercent,
                    ProjectID = o.ProjectID
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProjectDropdownDTO>> GetProjectsForDropdownAsync()
        {
            return await _context.projects
                .Select(p => new ProjectDropdownDTO
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName
                })
                .ToListAsync();
        }

        public async Task UpdateOperationalObjectiveAsync(
        int id,
        AddOperationalObjectiveDTO dto)
        {
            var entity = await _context.OperationalObjectives.FindAsync(id);
            if (entity == null) return;

            entity.OperationalObjectiveName = dto.OperationalObjectiveName;
            entity.Baseline_Value = dto.Baseline_Value;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            entity.ResponsiblePerson = dto.ResponsiblePerson;
            entity.KPI = dto.KPI;
            entity.QualitativeOutput = dto.QualitativeOutput;
            entity.QuantitativeOutput = dto.QuantitativeOutput;
            entity.AchievementPercent = dto.AchievementPercent;

            await _context.SaveChangesAsync();
        }


    }
}
