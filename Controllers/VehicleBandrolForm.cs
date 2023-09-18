using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeBandrol.Data.Enum;
using ProjeBandrol.Data.Services;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;
using System.Diagnostics;

namespace ProjeBandrol.Controllers
{
    [Authorize]
    public class VehicleBandrolForm : Controller
    {
        private readonly UserManager<Models.AppUser> _userManager;
        private readonly IVehicleBandrolService _service;


        public VehicleBandrolForm(IVehicleBandrolService service, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _service = service;
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleBandrolVM vehicleBandrolVM)
        {      
            var userId = _userManager.GetUserId(HttpContext.User);

            if (ModelState.IsValid)
            {
                 await _service.AddNewVehicleAsync(userId,vehicleBandrolVM);
                return RedirectToAction("Index", "Home");
            }

            return View();

        }
        public IActionResult AracListe()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var aracBilgileri = _service.GetAllVehicle().Where(model => model.UserId == userId);
            
            var aracBilgileriVM = aracBilgileri.Select(entity => new VehicleBandrolVM
            {
               Plaka= entity.Plaka,
               Model = entity.Model,
               Renk= entity.Renk,
               SeriNo= entity.SeriNo,
               BandrolDurumuTxt= entity.BandrolDurumuTxt,
               
               
            }).ToList();
            return View(aracBilgileriVM);
        }

        public async Task<IActionResult> BandrolListe()
        {
            var userId = _userManager.GetUserId(HttpContext.User);

            var bandrol = await _service.GetAllBandrols(userId);
                

          
            return View(bandrol);
        }
        public IActionResult BandrolBasvurusu()
        {

            var userId = _userManager.GetUserId(HttpContext.User);

            var aracBilgileri = _service.GetAllVehicle().Where(model => model.UserId == userId );

            var aracBilgileriVM = aracBilgileri.Select(entity => new VehicleBandrolVM
            {
                Id= entity.Id,
                Plaka = entity.Plaka,
                SeriNo = entity.SeriNo,
                KullaniciTipi = entity.KullaniciTipi,


            }).ToList();
            return View(aracBilgileriVM);
        }

        [HttpPost]
        public IActionResult BandrolBasvurusu(int aracId)
        {
            
            var arac1 = _service.GetVehicleById(aracId);
            if (arac1 == null)
            {          
                return RedirectToAction("Error");
            }
            return RedirectToAction("Odeme", new {aracId= arac1.Id, aracKullaniciTipi =arac1.KullaniciTipi});
          
        }


        public async Task<IActionResult> Odeme(int aracId, UserType aracKullaniciTipi)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            var personelOdeme = await _service.PersonelOdeme(userId);
            ViewBag.PersonelOdeme = personelOdeme;
            ViewBag.AracKullaniciTipi = aracKullaniciTipi;
            ViewBag.AracId = aracId;
            return View();
          
        }

        [HttpPost]
        public async Task<IActionResult> Odeme(OdemeVM odemeVM)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
               
            if (ModelState.IsValid)
            {


                await _service.AddNewBandrolAsync(userId, odemeVM.aracId,odemeVM.KullaniciTipi);
                await _service.AddNewPaymentAsync(odemeVM, userId, odemeVM.aracId);
                return RedirectToAction("Index", "Home");

            }
            return View();

        }

        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
