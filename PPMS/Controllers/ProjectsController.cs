using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace PPMS.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // GET: View all projects
        [HttpGet]
        public async Task<IActionResult> ViewAllProjects(string? search)
        {
            var projects = await _projectService.GetAllProjectsAsync(search);
            return View(projects);
        }

        // GET: Add new project form
        [HttpGet]
        public async Task<IActionResult> AddNewProject(int? projectTypeID)
        {
            byte fundTypeID = (byte)(projectTypeID ?? 1);

            var model = new AddNewProjectDTO
            {
                FundType = fundTypeID,
                MainPillars = await _projectService.GetAllMainPillarsAsync()
            };

            await LoadDropdowns(fundTypeID);

            return View(model);
        }

        // POST: Save project (Draft / Final)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewProject(AddNewProjectDTO model)
        {
            if (!ModelState.IsValid)
            {
                model.MainPillars = await _projectService.GetAllMainPillarsAsync();
                await LoadDropdowns((byte)model.FundType);
                return View(model);
            }

            int projectId = await _projectService.AddNewProjectAsync(model);

            TempData[model.IsDraft ? "InfoMessage" : "SuccessMessage"] =
                model.IsDraft ? "📝 Project saved as Draft." : "✅ Project saved successfully!";

            return RedirectToAction(nameof(ViewAllProjects));
        }

        // Load dropdowns helper
        private async Task LoadDropdowns(byte fundType)
        {
            ViewBag.Participants = await _projectService.GetAllParticipantsAsync();
            ViewBag.Coordinators = await _projectService.GetAllCoordinatorsAsync();
            ViewBag.PillarLeads = await _projectService.GetAllPillarLeadsAsync();
            ViewBag.FundingAgencies = await _projectService.GetFundingAgenciesByTypeAsync(fundType);
            ViewBag.MeasurementMethods = await _projectService.GetMeasurementMethodsAsync();

            ViewBag.Currencies = new SelectList(
                Enum.GetValues(typeof(Project.enCurrency))
                    .Cast<Project.enCurrency>()
                    .Select(c => new { Value = (int)c, Text = c.ToString() }),
                "Value", "Text"
            );
        }

        // GET: Return Sub-Indicators for a Main Pillar
        [HttpGet]
        public async Task<IActionResult> GetSubKPIs(int mainPillarID)
        {
            var subKpis = await _projectService.GetSubKpiByMainPillar(mainPillarID);
            return Json(subKpis);
        }
    }
}

