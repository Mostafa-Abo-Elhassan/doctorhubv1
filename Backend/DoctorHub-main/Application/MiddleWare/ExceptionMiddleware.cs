using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using ILogger = Serilog.ILogger;

namespace Application.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _environment; // Add this field

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment) // Add IHostEnvironment parameter
        {
            _next = next;
            _logger = Log.Logger;
            _environment = environment; // Assign the environment
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            _logger.Error(exception, "An unhandled exception occurred: {Message}", exception.Message);

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred. Please try again later.",
                Detailed = _environment.IsDevelopment() ? exception.Message : null // Use _environment instead of App.Environment
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}