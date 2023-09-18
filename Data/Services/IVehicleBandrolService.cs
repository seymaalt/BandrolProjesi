using ProjeBandrol.Data.Enum;
using ProjeBandrol.Data.ViewModels;
using ProjeBandrol.Models;

namespace ProjeBandrol.Data.Services
{
    public interface IVehicleBandrolService
    {
        Task AddNewVehicleAsync(string userId,VehicleBandrolVM data);

        Task AddNewBandrolAsync(string userId, int aracId, UserType KullaniciTipi);

        Task AddNewPaymentAsync(OdemeVM odemeVM, string userId, int aracId);

        List<Vehicle> GetAllVehicle();

        Task<List<BandrolListeVM>> GetAllBandrols(string userId);
        string GenerateUserId(UserType KullaniciTipi);

        Vehicle GetVehicleById(int aracId);

        Task<bool> PersonelOdeme(string userId);
    }
}
