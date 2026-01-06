using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PPMS.ViewModels;

namespace PPMS.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;

        public UsersController(IUserService userService, IEmployeeService employeeService)
        {
            _userService = userService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> ViewAllUsers(string? search)
        {
            var users = await _userService.GetAllUsersAsync(search);
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            var vm = new CreateUserViewModel
            {
                Employees = await LoadEmployeesDropdownAsync()
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Employees = await LoadEmployeesDropdownAsync();
                return View(vm);
            }

            int result = await _userService.CreateUserAsync(vm.User);

            if (result == -1)
            {
                ModelState.AddModelError("User.Username", "This username already exists.");
                vm.Employees = await LoadEmployeesDropdownAsync();
                return View(vm);
            }

            TempData["SuccessMessage"] = "User created successfully!";
            return RedirectToAction(nameof(ViewAllUsers));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            int result = await _userService.DeleteUserAsync(id);

            if (result > 0)
                TempData["SuccessMessage"] = "User deleted successfully!";
            else
                TempData["ErrorMessage"] = "Failed to delete user.";

            return RedirectToAction(nameof(ViewAllUsers));
        }

        // Helper method
        private async Task<IEnumerable<SelectListItem>> LoadEmployeesDropdownAsync()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return employees.Select(e => new SelectListItem
            {
                Value = e.ID.ToString(),
                Text = e.Name
            }).ToList();
        }
    }
}
