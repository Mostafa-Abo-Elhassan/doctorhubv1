
namespace Domain.Interfaces
{
    public interface ILocationService
    {
        //Task<(string DriverLocation, string ETA, string Route)> GetDriverLocation(string driverId, string startLocation, string endLocation);

        //Task<DriverLocation> GetDriverLocation(string driverId, string startLocation, string endLocation);

        Task<(double Latitude, double Longitude)> GetLatLng(string address);

        // Add the missing method definition to fix CS1061  
        Task<double> GetDistanceInKm(string startLocation, string endLocation);
    }
}
