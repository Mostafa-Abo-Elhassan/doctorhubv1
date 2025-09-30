using Application;
using Application.Hubs;
using Application.MiddleWare;
using Application.Middlewares;
using Domain.Entities;
using Domain.Service;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add Services to Container

            builder.Services.AddControllers();

            // Add HttpClientFactory for IHttpClientFactory (this resolves the issue)
            //builder.Services.AddHttpClient();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            #endregion

            #region Configure DbContext

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            #endregion

            #region Configure Identity

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                // Optional: Configure Identity options (e.g., password requirements)
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<User>>(); // Explicitly add SignInManager<User>

            #endregion

            #region Configure CORS

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigins", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();

                    //.WithOrigins(
                    //    "http://localhost:3000", // Allow your frontend origin (e.g., React app)
                    //    "https://yourfrontenddomain.com" // Add your production frontend domain
                    //)
                    //.AllowAnyHeader()
                    //.AllowAnyMethod()
                    //.AllowCredentials(); // If you need to send cookies or auth headers
                });
            });

            #endregion

            #region Configure Swagger

            //builder.Services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo
            //    {
            //        Title = "TripPool API",
            //        Version = "v1",
            //        Description = "API documentation for TripPool ride-sharing platform"
            //    });

            //    // ✅ تعريف Bearer Token
            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Name = "Authorization",
            //        Type = SecuritySchemeType.Http,
            //        Scheme = "Bearer",
            //        BearerFormat = "JWT",
            //        In = ParameterLocation.Header,
            //        Description = "أدخل التوكن بالشكل ده: Bearer {your token}"
            //    });

            //    // ✅ ربط التوكن بالـ endpoints كلها
            //    c.AddSecurityRequirement(new OpenApiSecurityRequirement
            //    {
            //        {
            //            new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            Array.Empty<string>()
            //        }
            //    });
            //});
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DoctorHub",
                    Version = "v1",
                    Description = "API documentation for DoctorHub"
                });

                // Use full type name as schemaId to avoid conflicts
                c.CustomSchemaIds(type => type.FullName);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "أدخل التوكن بالشكل ده: Bearer {your token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            #endregion

            #region Register Dependencies and Services

            // Register dependencies
            builder.Services
                .AddInfrastractureDependencess()
                .AddServiceDependences()
                .AddCoreDependencess();

            #endregion

            #region Configure SignalR

            builder.Services.AddSignalR();

            #endregion

            #region Configure AddMemoryCache

            // Program.cs or Startup.cs (where you configure services)
            builder.Services.AddMemoryCache();

            #endregion

            #region Configure JWT Authentication and External Providers

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            })

            .AddFacebook(options =>
            {
                options.AppId = builder.Configuration["FacebookSettings:AppId"];
                options.AppSecret = builder.Configuration["FacebookSettings:AppSecret"];
            });

            #endregion

            #region Configure Serilog

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(builder.Configuration)
            //    .WriteTo.Console()
            //    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            //builder.Host.UseSerilog();

            #endregion





            // Add this line before builder.Build()
            builder.Host.UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration)
                             .ReadFrom.Services(services)
            );

            // Register Serilog.ILogger as a singleton
            builder.Services.AddSingleton(Log.Logger);
            var app = builder.Build();

            #region Swagger Middleware

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve Swagger UI (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            #endregion

            #region Development environment

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            #endregion


            #region Custom Middlewares

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            #endregion

            #region HTTP Pipeline

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.MapOpenApi();
            }

            app.UseCors("AllowSpecificOrigins"); // Apply the CORS policy

            app.MapHub<TrackingHub>("/trackingHub");
            app.MapHub<NotificationHub>("/notificationHub");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            #endregion

            #region Serilog Request Logging

            app.UseSerilogRequestLogging();

            #endregion

            app.Run();
        }
    }
}
