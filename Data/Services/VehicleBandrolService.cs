using Microsoft.EntityFrameworkCore;
using ProjeBandrol.Data.Enum;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data.Services
{
    public class VehicleBandrolService : IVehicleBandrolService
    {
        private readonly AppDbContext _context;

        public VehicleBandrolService(AppDbContext context )
        {
            _context = context;
        }

        public async Task AddNewBandrolAsync(string userId, int aracId,UserType KullaniciTipi)
        {
            var newBandrol = new Bandrol()
            {
                BandrolNo = GenerateUserId(KullaniciTipi),
                UserId = userId,
                VehicleId = aracId,
                BasvuruDurumu = ApplicationStatusEnum.OnayBekleniyor,
                Aktiflik = false
            };
            await _context.Bandrols.AddAsync(newBandrol);
            await _context.SaveChangesAsync();
        }

        public async Task AddNewPaymentAsync(OdemeVM odemeVM, string userId,int aracId)
        {
            var payment = new Payment()
            {
                DekontNo = odemeVM.DekontNo,
                UserId = userId,
                VehicleId = aracId,
            };
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
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


        public string GenerateUserId(UserType KullaniciTipi)
        {
            string prefix = "";
            switch (KullaniciTipi)
            {
                case UserType.Misafir:
                    prefix = "G";
                    break;
                case UserType.Ogrenci:
                    prefix = "S";
                    break;
                case UserType.Personel:
                    prefix = "E";
                    break;
                    // Diğer kullanıcı türlerine göre case'ler ekleyebilirsiniz
            }

            // Veritabanında bu türden kaç kullanıcı olduğunu alın
            int userCount = _context.Vehicles.Count(u => u.KullaniciTipi == KullaniciTipi);

            // Yeni kullanıcı ID'sini oluşturun
            string userId = $"{prefix}{userCount + 1:D4}"; // D4, dört basamaklı sayıyı temsil eder

            return userId;
        }


        public Vehicle GetVehicleById(int aracId )
        {
            return _context.Vehicles.FirstOrDefault(v => v.Id == aracId);
        }


        public List<Vehicle> GetAllVehicle()
        {
            var aracBilgileri = _context.Vehicles.ToList();
            return aracBilgileri;

        }

        public async Task<List<BandrolListeVM>> GetAllBandrols(string userId)
        {
            var bandrol = await _context.Bandrols
                .Include(b => b.Vehicle)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            var bandrolListeVM = bandrol.Select(entity => new BandrolListeVM
            {
                BandrolNo = entity.BandrolNo,
                BaslangicTarihi = entity.BaslangicTarihi,
                BasvuruDurumu = entity.BasvuruDurumu,
                BitisTarihi = entity.BitisTarihi,
                Aktiflik = entity.Aktiflik,
                Plaka = entity.Vehicle.Plaka
            }).ToList();

            return bandrolListeVM;
        }

        public async Task<bool> PersonelOdeme(string userId)//true dönerse ödeme yapmayacak
        {
            var result = await _context.Users.Where(U => U.Id == userId && U.Bandrols == null).FirstOrDefaultAsync();

            if(result == null)
                return false;

            return true;
        }

    }
}
