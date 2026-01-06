using BL.Interfaces;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PPMS.ViewModels;

namespace PPMS.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IGroupService _groupService;

        public RolesController(IRoleService roleService, IGroupService groupService)
        {
            _roleService = roleService;
            _groupService = groupService;
        }

        public async Task<IActionResult> AddRolesToGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            ViewBag.Groups = new SelectList(groups, "GroupID", "GroupName");

            var roles = await _roleService.GetPagesRolesAsync();

            var model = new RolesViewModel
            {
                Roles = roles
            };

            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddRolesToGroups(RolesViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        var groups = await _groupService.GetAllGroupsAsync();
        //        ViewBag.Groups = new SelectList(groups, "GroupID", "GroupName");
        //        return View(model);
        //    }

        //    // تحديث كل الـ Roles المحددة مع GroupID المختار
        //    foreach (var role in model.Roles)
        //    {
        //        role.GroupID = model.SelectedGroupID;
        //        await _roleService.AddRolesToGroupsAsync(role);
        //    }

        //    TempData["SuccessMessage"] = "Roles have been successfully assigned to the selected group.";
        //    return RedirectToAction(nameof(AddRolesToGroups));
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdatePermission(int roleId, int pageId, string field, bool value)
        //{
        //    var roleDto = new RoleDTO
        //    {
        //        RoleID = roleId,
        //        PageID = pageId
        //    };

        //    await _roleService.UpdatePermissionAsync(roleDto, field, value);
        //    return Json(new { success = true });
        //}
    }
}
