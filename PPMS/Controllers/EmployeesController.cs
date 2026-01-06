using Microsoft.AspNetCore.Mvc;
using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authorization;

namespace PPMS.Controllers
{
    //[Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDirectorateService _directorateService;
        private readonly IGroupService _groupService;

        public EmployeesController(
            IEmployeeService employeeService, 
            IDirectorateService directorateService,
            IGroupService groupService)
        {
            _employeeService = employeeService;
            _directorateService = directorateService; // ✅ inject it
            _groupService = groupService;
        }


        [HttpGet]
        public async Task<IActionResult> AddNewEmployee()
        {
            // Directorates dropdown
            var directorates = await _directorateService.ViewAllIDirectoratesAsync();
            ViewBag.Directorates = new SelectList(
                directorates,
                "DirectorateID",   // value field
                "DirectorateName"  // text field
            );

            // Groups dropdown
            var groups = await _groupService.GetAllGroupsAsync();
            ViewBag.Groups = new SelectList(
                groups,
                "GroupID",        // value field from GroupDTO
                "GroupName"  // text field from GroupDTO
            );

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddNewEmployee(EmployeeDTO employeeDto)
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(employeeDto.Name))
            {
                ModelState.AddModelError("Name", "Employee Name is required.");

                // repopulate dropdown if validation fails
                var directorates = await _directorateService.ViewAllIDirectoratesAsync();
                ViewBag.Directorates = directorates
                    .Select(d => new SelectListItem
                    {
                        Value = d.DirectorateID.ToString(),
                        Text = d.DirectorateName
                    }).ToList();

                return View(employeeDto);
            }

            int result = await _employeeService.AddNewEmployeeAsync(employeeDto);

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Employee added successfully!";
                return RedirectToAction(nameof(ViewAllEmployees));
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to add employee.";

                // repopulate dropdown before returning
                var directorates = await _directorateService.ViewAllIDirectoratesAsync();
                ViewBag.Directorates = directorates
                    .Select(d => new SelectListItem
                    {
                        Value = d.DirectorateID.ToString(),
                        Text = d.DirectorateName
                    }).ToList();

                return View(employeeDto);
            }
        }

        
        [HttpGet]
        public async Task<IActionResult> ViewAllEmployees()
        {
            var allEmployees = await _employeeService.GetAllEmployeesAsync();
           
            return View(allEmployees);
        }
    }
}

