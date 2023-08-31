using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeBandrol.Data.Services;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;
using System.Diagnostics;

namespace ProjeBandrol.Controllers
{
    [Authorize]
    public class VehicleBandrolForm : Controller
    {
        private readonly UserManager<AppUser> _userManager;
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
                await _service.AddNewVehicleAsync( userId,vehicleBandrolVM);
                return RedirectToAction(nameof(Index1));
                //return View(vehicleBandrolVM);

            }
            //if (!string.IsNullOrEmpty(userId))
            //{
            // userId'yi kullanarak gerekli işlemleri yapabilirsiniz
            // ...
            //await _service.AddNewVehicleAsync(vehicleBandrolVM, userId);
            //return RedirectToAction(nameof(Index1));
            //  }
            // return RedirectToAction(nameof(Index));
            return View(vehicleBandrolVM);

        }
        public IActionResult Index1()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
