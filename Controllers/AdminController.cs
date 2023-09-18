using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjeBandrol.Data.Services;
using ProjeBandrol.Data.ViewModels;

namespace ProjeBandrol.Controllers
{
    [Authorize(Roles ="Yonetici")]
    public class AdminController : Controller
    {
        private readonly IAdminService _service;

        public AdminController(IAdminService service) {
        
            _service = service;
        }
        public async Task<IActionResult> BandrolOnaylaListe()
        {
            var result = await  _service.BandrolOnaylaListe();
            return View(result);
        }


        [HttpPost]
        public async Task<IActionResult> BandrolOnaylaListe(BandrolOnaylaListVM bandrolOnaylaVM)
        {

            return RedirectToAction(nameof(BandrolOnayla),bandrolOnaylaVM);
        }


        public async Task<IActionResult> BandrolOnayla(BandrolOnaylaListVM bandrolOnaylaListVM)
        {
            var Result = await _service.GetPaymentByVehicleId(bandrolOnaylaListVM.aracId);
           
            return View(Result);
        }

        [HttpPost]
        public async Task<IActionResult> BandrolOnayla(BandrolVM bandrolVM)
        {
            if(ModelState.IsValid)
            {
              
               await _service.BandrolOnayla(bandrolVM);

                return RedirectToAction("Index", "Home");

            }


            return View();
        }
      
    }
}
