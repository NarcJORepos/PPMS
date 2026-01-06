using BL.Interfaces;
using DAL.Models.Entities;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace PPMS.Controllers
{
    public class FundingAgencyController : Controller
    {
        private readonly IFundingAgencyService _fundingAgencyService;

        public FundingAgencyController(IFundingAgencyService fundingAgencyService)
        {
            this._fundingAgencyService = fundingAgencyService;
        }
        public async Task<IActionResult> ViewAllFundingAgencies()
        {
            var AllFundingAgencies = await _fundingAgencyService.GetAllFundingAgenciesAsync();

            return View(AllFundingAgencies);
        }


        public IActionResult AddNewFundingAgencyAsync()
        { 
          return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFundingAgencyAsync(AddFundingAgencyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = await _fundingAgencyService.AddNewFundingAgencyAsync(dto);

            if (result > 0)
            {
                return RedirectToAction(nameof(ViewAllFundingAgencies));
            }

            else
            {
                ViewBag.ErrorMessage = "Failed to add Funding Agency.";
                return View(dto);
            
            }
        }
    }
}
