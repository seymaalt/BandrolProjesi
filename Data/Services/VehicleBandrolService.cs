using BandrolSistemi.Models;
using Microsoft.AspNetCore.Identity;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data.Services
{
    public class VehicleBandrolService : IVehicleBandrolService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public VehicleBandrolService(AppDbContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
      
        
        public async Task AddNewVehicleAsync(string userId,VehicleBandrolVM data )
        {
            var newVehicle = new Vehicle()
            {
                Id = data.Id,
                SeriNo = data.SeriNo,
                Renk = data.Renk,
                Model = data.Model,
                Plaka = data.Plaka,
                BandrolDurumu = data.BandrolDurumu,
                BandrolDurumuTxt = data.BandrolDurumuTxt,
                KullaniciTipi = data.KullaniciTipi,
                UserId = userId

            };
            await _context.Vehicles.AddAsync(newVehicle);
            await _context.SaveChangesAsync();

        }
    }
}
