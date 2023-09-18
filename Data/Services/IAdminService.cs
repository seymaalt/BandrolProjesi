using ProjeBandrol.Data.Enum;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data.Services
{
    public interface IAdminService
    {
        Task<List<Bandrol>> BandrolOnaylaListe();

        Task<BandrolOnaylaVM> GetPaymentByVehicleId(int aracId);

        Task BandrolOnayla(BandrolVM bandrolVM);
    }
}
