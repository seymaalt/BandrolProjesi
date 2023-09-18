using Microsoft.EntityFrameworkCore;
using ProjeBandrol.Data.Enum;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task BandrolOnayla(BandrolVM bandrolVM)
        {
            var bandrol = await _context.Bandrols.FirstOrDefaultAsync(b => b.Id == bandrolVM.BandrolId);

            if (bandrolVM.BasvuruDurumu == ApplicationStatusEnum.Onaylandi)
            {
                bandrol.Aktiflik = true;
                bandrol.BaslangicTarihi = DateTime.Now;
                bandrol.BitisTarihi = DateTime.Now.AddYears(2);
                bandrol.BasvuruDurumu = ApplicationStatusEnum.Onaylandi;
            }
            else if (bandrolVM.BasvuruDurumu == ApplicationStatusEnum.Reddedildi)
            {
                bandrol.Aktiflik = false;
                bandrol.BasvuruDurumu = ApplicationStatusEnum.Reddedildi;
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<Bandrol>> BandrolOnaylaListe()
        {
            var result = await _context.Bandrols
                .Include(b=>b.User)
                .Include(b => b.Vehicle)
                .Where(b=>b.Aktiflik==false && b.BasvuruDurumu== Enum.ApplicationStatusEnum.OnayBekleniyor)
                .ToListAsync();

            return result;

        }



        public async Task<BandrolOnaylaVM> GetPaymentByVehicleId(int aracId)
        {
            var result = await _context.Vehicles
                       .Include(v => v.Bandrol)     // Araca bağlı ödemeyi dahil edin
                       .Include(v => v.User)        // Araca bağlı kullanıcıyı dahil edin
                       .Include(v => v.Payment)     // Araca bağlı ödemeyi dahil edin
                       .FirstOrDefaultAsync(v => v.Id == aracId);

            if (result == null)
            {
                return null; // Aracı bulamazsanız null dönebilirsiniz veya uygun bir hata işleme stratejisi kullanabilirsiniz.
            }

            var bandrolOnayla = new BandrolOnaylaVM()
            {
                Ad = result.User.Ad,
                Soyad = result.User.Soyad,
                TcKimlikNo = result.User.TcKimlikNo,
                SeriNo = result.SeriNo,
                DekontNo = result.Payment.DekontNo, // Payment özelliğine erişim ekledik
                KullaniciTipi = result.KullaniciTipi,
                Model = result.Model,
                Renk = result.Renk,
                Plaka = result.Plaka,
                BandrolId = result.Bandrol.Id,
                BasvuruDurumu = result.Bandrol.BasvuruDurumu
            };

            return bandrolOnayla;
        }


    }
}
