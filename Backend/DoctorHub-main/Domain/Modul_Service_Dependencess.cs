using Microsoft.Extensions.DependencyInjection;


namespace Domain.Service;

public static class Modul_Service_Dependences
{
    public static IServiceCollection AddServiceDependences(this IServiceCollection services)
    {
        //services.AddScoped<IEmailService, EmailService>();
        //services.AddScoped<ILocationService, LocationService>();
        ////services.AddScoped<IDriverService, DriverService>();
        ////services.AddScoped<IRiderService, RiderService>();
        ////services.AddScoped<IRideMatchingService, RideMatchingService>();
        ////services.AddScoped<ITripCostService, TripCostService>();
        ////services.AddScoped<ITripCostService, TripCostService>();
        //services.AddScoped<IautheService, AutheService>();
        //services.AddScoped<IOtpService, OtpService>();



        return services;
    }
}