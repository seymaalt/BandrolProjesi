using ProjeBandrol.Data.ViewModels;


namespace ProjeBandrol.Data.Services
{
    public interface IVehicleBandrolService
    {
        Task AddNewVehicleAsync(string userId,VehicleBandrolVM data);

    }
}
