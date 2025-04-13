using BE_ToDoListApp.Application.Interfaces;
using BE_ToDoListApp.Infrastructure.Data;
using BE_ToDoListApp.Infrastructure.Repositories;
using BE_ToDoListApp.SharedLibrary.DependencyInjections;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BE_ToDoListApp.Application.Services.Interfaces;
using BE_ToDoListApp.Application.Services.Implements;
using Microsoft.AspNetCore.Http;
using BE_ToDoListApp.Infrastructure.BackGroundServices.Services;
using BE_ToDoListApp.Infrastructure.BackGroundServices.BackgroundJobs;

namespace BE_ToDoListApp.Infrastructure.DependencyInjections
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            //Add database connectivity
            //Add authentication scheme
            SharedServiceContainer.AddSharedServices<ToDoListDBContext>(services, config, config["MySerilog:FileName"]!);

            # region Dependency Injection (DI)
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //Setup UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Background Services
            services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            services.AddHostedService<QueuedHostedService>();

            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IToDoTaskService, ToDoTaskService>();
            services.AddScoped<ITaskStatisticService, TaskStatisticService>();
            #endregion
            #endregion

            return services;
        }

        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder application)
        {
            //Register middleware
            SharedServiceContainer.UseSharedPolicies(application);

            return application;
        }
    }
}
