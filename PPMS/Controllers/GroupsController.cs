using BL.Interfaces;
using BL.Services;
using DAL.Data;
using DAL.Models.Entities;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PPMS.Controllers
{
    public class GroupsController : Controller
    {
        private readonly IGroupService _groupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
       
        public async Task<IActionResult> ViewAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();

            return View(groups);
        }

       
        public IActionResult AddNewGroup()
        {
            return View();
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewGroup(GroupDTO groupDto)            
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(groupDto.GroupName))
            {
                ModelState.AddModelError("Group Name", "Group Name is required.");
                return View(groupDto);
            }

            int result = await _groupService.AddNewGroupAsync(groupDto);

            if (result > 0)
            {
               return RedirectToAction(nameof(ViewAllGroups));
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to add group.";
                return View(groupDto);
            }
        }  
    }
}
