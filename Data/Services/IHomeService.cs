namespace ProjeBandrol.Data.Services
{
    public interface IHomeService
    {
        Task<int> VehicleCount();
        Task<int> BandrolCount();
        Task<int> UserCount();
    }
}
