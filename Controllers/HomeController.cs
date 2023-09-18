using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeBandrol.Data.Services;
using ProjeBandrol.Models;
using System.Diagnostics;

namespace ProjeBandrol.Controllers
{
 
    public class HomeController : Controller
    {
      
        private readonly IHomeService _service;

        public int SecilenArac { get; private set; }

        public HomeController(IHomeService service)
        {
          
            _service = service;
        }
      
        public async Task<IActionResult> Index()
        {
            ViewBag.VehicleCount = await _service.VehicleCount();
            ViewBag.UserCount = await _service.UserCount();
            ViewBag.BandrolCount = await _service.BandrolCount();
            return View();
        }

      
    }
}