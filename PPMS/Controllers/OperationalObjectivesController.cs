using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BL.Interfaces;
using DTO;
using System.Threading.Tasks;
using DAL.Models.Entities;

namespace PPMS.Controllers
{
    public class OperationalObjectivesController : Controller
    {
        private readonly IOperationalObjectiveService _objectiveService;

        public OperationalObjectivesController(IOperationalObjectiveService objectiveService)
        {
            _objectiveService = objectiveService;
        }

        // ===============================
        // STEP 1: Select Project
        // ===============================
        [HttpGet]
        public async Task<IActionResult> SelectProject()
        {
            var projects = await _objectiveService.GetProjectsForDropdownAsync();
            ViewBag.ProjectList = new SelectList(projects, "ProjectID", "ProjectName");
            return View();
        }

        [HttpPost]
        public IActionResult SelectProject(int projectID)
        {
            if (projectID == 0)
            {
                ModelState.AddModelError("", "الرجاء اختيار مشروع");
                return View();
            }

            return RedirectToAction(nameof(GetAllOperationalObjectives), new { projectID });
        }

        // ===============================
        // LIST BY PROJECT
        // ===============================
        [HttpGet]
        public async Task<IActionResult> GetAllOperationalObjectives(int projectID)
        {
            var objectives = await _objectiveService.GetAllOperationalObjectivesAsync(projectID);
            ViewBag.ProjectID = projectID;
            return View(objectives);
        }

        // ===============================
        // CREATE (GET)
        // ===============================
        [HttpGet]
        public async Task<IActionResult> AddNewOperationalObjective()
        {
            await LoadProjects();
            return View(new AddOperationalObjectiveDTO());
        }

        // ===============================
        // CREATE (POST)
        // ===============================
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewOperationalObjective(AddOperationalObjectiveDTO model)
        {
            if (!ModelState.IsValid)
            {
                await LoadProjects(); // إعادة تحميل المشاريع عند الخطأ
                return View(model);
            }

            foreach (var obj in model.Objectives)
            {
                await _objectiveService.AddNewOperationalObjectiveAsync(new AddOperationalObjectiveDTO
                {
                    OperationalObjectiveName = obj,
                    ProjectID = model.ProjectID,
                    Baseline_Value = model.Baseline_Value,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    ResponsiblePerson = model.ResponsiblePerson,
                    KPI = model.KPI,
                    QualitativeOutput = model.QualitativeOutput,
                    QuantitativeOutput = model.QuantitativeOutput,
                    AchievementPercent = model.AchievementPercent
                });
            }

            return RedirectToAction("Success");
        }



        private async Task LoadProjects()
        {
            var projects = await _objectiveService.GetProjectsForDropdownAsync();
            ViewBag.ProjectList = new SelectList(projects, "ProjectID", "ProjectName");
        }

        public IActionResult Success()
        {
            return View();
        }
        // ===============================
        // EDIT (GET)
        // ===============================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var obj = await _objectiveService.GetOperationalObjectiveByIdAsync(id);
            if (obj == null) return NotFound();

            var model = new AddOperationalObjectiveDTO
            {
                ProjectID = obj.ProjectID,
                OperationalObjectiveName = obj.OperationalObjectiveName,
                Baseline_Value = obj.Baseline_Value,
                StartDate = obj.StartDate,
                EndDate = obj.EndDate,
                ResponsiblePerson = obj.ResponsiblePerson,
                KPI = obj.KPI,
                QualitativeOutput = obj.QualitativeOutput,
                QuantitativeOutput = obj.QuantitativeOutput,
                AchievementPercent = obj.AchievementPercent
            };

            ViewBag.ObjectiveID = id;
            return View(model);
        }

        // ===============================
        // EDIT (POST)
        // ===============================        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddOperationalObjectiveDTO dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _objectiveService.UpdateOperationalObjectiveAsync(id, dto);

            return RedirectToAction(nameof(GetAllOperationalObjectives),
                new { projectID = dto.ProjectID });
        }


        // ===============================
        // DELETE
        // ===============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int projectID)
        {
            //await _objectiveService.DeleteOperationalObjectiveAsync(id);

            return RedirectToAction(nameof(GetAllOperationalObjectives),
                new { projectID });
        }
    }
}
