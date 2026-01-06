using BL.Interfaces;
using BL.Services;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace PPMS.Controllers
{
    public class DirectoratesController : Controller
    {
        private readonly IDirectorateService _directorateService;

        public DirectoratesController(IDirectorateService directorateService)
        {
            this._directorateService = directorateService;
        }
        public IActionResult AddNewDirectorate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDirectorate(DirectorateDTO dto) 
        {
            if (!ModelState.IsValid || string.IsNullOrWhiteSpace(dto.DirectorateName))
            {
                ModelState.AddModelError("Directorate Name", "Directorate Name is required.");
                return View(dto);
            }

            int result = await _directorateService.AddNewDirectorateAsync(dto);

            if (result > 0)
            {  
                return RedirectToAction(nameof(ViewAllDirectorates));
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to add directorate.";
                return View(dto);
            }
        }

        public async Task<IActionResult> ViewAllDirectorates()
        {
            var allDirectorates = await _directorateService.ViewAllIDirectoratesAsync();

            return View(allDirectorates);
        }
    }   
}
