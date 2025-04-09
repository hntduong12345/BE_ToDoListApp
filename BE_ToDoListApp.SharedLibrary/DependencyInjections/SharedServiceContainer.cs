using BE_ToDoListApp.SharedLibrary.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_ToDoListApp.SharedLibrary.DependencyInjections
{
    public static class SharedServiceContainer
    {
        public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services,
           IConfiguration config, string fileName) where TContext : DbContext
        {
            //Generic Database Context
            services.AddDbContext<TContext>(option => option.UseSqlServer(
                config.GetConnectionString("ToDoConnection"),
                sqlServerOption => sqlServerOption.EnableRetryOnFailure()));

            //Config SeriLog logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.File(path: $"{fileName}-.text",
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                outputTemplate: "{Timestamp: yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{NewLine}{Exception}",
                rollingInterval: RollingInterval.Day)
                .CreateLogger();

            //Add JWT Authentication Scheme
            JWTAuthenticationScheme.AddJwtAuthenticationScheme(services, config);

            return services;
        }

        public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
        {
            //Use Global Exception
            app.UseMiddleware<GlobalException>();

            return app;
        }
    }
}
