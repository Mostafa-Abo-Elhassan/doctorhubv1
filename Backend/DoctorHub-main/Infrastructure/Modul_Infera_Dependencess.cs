using Domain.AutheServices;
using Domain.Interfaces;
using Infrastructure.GenericRepositoryPattern;
using Infrastructure.Implement;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.UnitOfWork;

namespace Infrastructure
{


    public static class Modul_Infrastracture_Dependencess
    {
        public static IServiceCollection AddInfrastractureDependencess(this IServiceCollection services)
        {

            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddScoped<IUnitOfWork, UnitofWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IautheService, AutheService>();
            services.AddScoped<IOtpService, OtpService>();
            //services.AddDbContext<ApplicationDbContext>(options =>
            //options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TripPoolDb;Trusted_Connection=True;"));
            services.AddSignalR();
            //services.AddTransient<IHubContext<TrackingHub>, HubContext<TrackingHub>>();

            return services;


        }
    }
}
