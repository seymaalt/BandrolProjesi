using Microsoft.EntityFrameworkCore;

namespace ProjeBandrol.Data.Services
{
    public class HomeService : IHomeService
    {
        private readonly AppDbContext _context;

        public HomeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> BandrolCount()
        {
            var result = await _context.Bandrols
                .Where(B => B.Aktiflik)
                .CountAsync(); // CountAsync kullanılmalı

            return result;
        }

        public async Task<int> UserCount()
        {
            var result = await _context.Users.CountAsync();
            return result;
        }

        public async Task<int> VehicleCount()
        {
            var result = await _context.Vehicles.CountAsync();
            return result;
        }
    }
}
