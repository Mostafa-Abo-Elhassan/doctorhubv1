using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace Infrastructure.Implement
{
    public class LocationService : ILocationService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;

        public LocationService(IConfiguration configuration)
        {
            _apiKey = configuration["GoogleMaps:ApiKey"];
            _httpClient = new HttpClient();
        }

        public async Task<double> GetDistanceInKm(string startLocation, string endLocation)
        {
            var startLatLng = await GetLatLng(startLocation);
            var endLatLng = await GetLatLng(endLocation);

            var directionsUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLatLng.Latitude},{startLatLng.Longitude}&destination={endLatLng.Latitude},{endLatLng.Longitude}&key={_apiKey}";
            var response = await _httpClient.GetStringAsync(directionsUrl);

            var json = JObject.Parse(response);
            if (json["status"]?.ToString() != "OK")
                throw new Exception("Failed to get directions: " + json["status"]);

            var route = json["routes"]?[0];
            var leg = route?["legs"]?[0];
            var distanceText = leg?["distance"]?["text"]?.ToString();

            if (string.IsNullOrEmpty(distanceText))
                throw new Exception("Distance information not found in response.");

            // Google returns distance as "12.3 km" or "7.6 mi"
            var parts = distanceText.Split(' ');
            if (parts.Length < 2)
                throw new Exception("Unexpected distance format.");

            double distanceValue = double.Parse(parts[0], System.Globalization.CultureInfo.InvariantCulture);
            string unit = parts[1].ToLower();

            // Convert miles to kilometers if needed
            if (unit.StartsWith("mi"))
                distanceValue *= 1.60934;

            return distanceValue;
        }


        //public async Task<(string DriverLocation, string ETA, string Route)> GetDriverLocation(string driverId, string startLocation, string endLocation)
        //{
        //    var startLatLng = await GetLatLng(startLocation);
        //    var endLatLng = await GetLatLng(endLocation);

        //    var directionsUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLatLng.Latitude},{startLatLng.Longitude}&destination={endLatLng.Latitude},{endLatLng.Longitude}&key={_apiKey}";
        //    var response = await _httpClient.GetStringAsync(directionsUrl);

        //    // تحليل الـ JSON باستخدام Newtonsoft.Json
        //    var json = JObject.Parse(response);
        //    if (json["status"]?.ToString() != "OK")
        //        throw new Exception("Failed to get directions: " + json["status"]);

        //    var route = json["routes"]?[0];
        //    var leg = route?["legs"]?[0];
        //    var eta = leg?["duration"]?["text"]?.ToString() ?? "Unknown";
        //    var routeSummary = route?["summary"]?.ToString() ?? "Unknown route";
        //    var driverLocation = "Current Location"; // يمكن تحديث هذا بناءً على بيانات حقيقية من السائق

        //    return (driverLocation, eta, routeSummary);
        //}



        //public async Task<DriverLocation> GetDriverLocation(string driverId, string startLocation, string endLocation)
        //{
        //    var startLatLng = await GetLatLng(startLocation);
        //    var endLatLng = await GetLatLng(endLocation);

        //    var directionsUrl = $"https://maps.googleapis.com/maps/api/directions/json?origin={startLatLng.Latitude},{startLatLng.Longitude}&destination={endLatLng.Latitude},{endLatLng.Longitude}&key={_apiKey}";
        //    var response = await _httpClient.GetStringAsync(directionsUrl);

        //    var json = JObject.Parse(response);
        //    if (json["status"]?.ToString() != "OK")
        //        throw new Exception("Failed to get directions: " + json["status"]);

        //    var route = json["routes"]?[0];
        //    var leg = route?["legs"]?[0];
        //    var eta = leg?["duration"]?["text"]?.ToString() ?? "Unknown";
        //    var routeSummary = route?["summary"]?.ToString() ?? "Unknown route";

        //    return new DriverLocation
        //    {
        //        Start = new GeoCoordinate(startLatLng.Latitude, startLatLng.Longitude),
        //        Destination = new GeoCoordinate(endLatLng.Latitude, endLatLng.Longitude),
        //        ETA = eta,
        //        Route = routeSummary
        //    };
        //}



        public async Task<(double Latitude, double Longitude)> GetLatLng(string address)
        {
            var geocodeUrl = $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={_apiKey}";
            var response = await _httpClient.GetStringAsync(geocodeUrl);

            // تحليل الـ JSON باستخدام Newtonsoft.Json
            var json = JObject.Parse(response);
            if (json["status"]?.ToString() != "OK")
                throw new Exception("Failed to geocode address: " + json["status"]);

            var location = json["results"]?[0]?["geometry"]?["location"];
            var latitude = (double?)location?["lat"] ?? 0.0;
            var longitude = (double?)location?["lng"] ?? 0.0;

            return (latitude, longitude);
        }
    }
}
