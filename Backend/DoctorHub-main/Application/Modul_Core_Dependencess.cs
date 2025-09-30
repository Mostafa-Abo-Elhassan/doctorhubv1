using Application.Behaviors;
using Application.Helpers;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Implement;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Modul_Core_Dependencess
    {
        public static IServiceCollection AddCoreDependencess(this IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            // Get Validators
            // Register FluentValidation Validators
            // Add JWT Service
            services.AddScoped<IJwtService, JwtService>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddHttpContextAccessor(); // إضافة IHttpContextAccessor
            services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();


            return services;
        }
    }
}
