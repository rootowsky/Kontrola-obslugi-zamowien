using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using KOZ.API.Data.DbContexts;
using KOZ.API.Data.Repositories;
using KOZ.API.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using KOZ.API.Data.DataClasses;

namespace KOZ.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("OrdersContext");
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped(p => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(Assembly.GetExecutingAssembly().GetName().Name);
            }).CreateMapper());
            services.AddScoped<IOrdersRepository, OrdersRepository>();
            services.AddScoped<IRepository<Worker>, WorkersRepository>();
            services.AddDbContext<OrdersContext>(
                options => options.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            TryUpdateDatabase(app);
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMvc();
        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (IServiceScope serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<OrdersContext>().Database.Migrate();
            }
        }

        /*
        This is only for development of this project.
        On production I would use container orchestrator
        to take care of connection resillency.
        */
        private static void TryUpdateDatabase(IApplicationBuilder app)
        {
            const int waitTimeSeconds = 1;
            const int connectionAttemptsNumber = 100;
            for (var i = 0; i < connectionAttemptsNumber; i++)
            {
                try
                {
                    UpdateDatabase(app);
                }
                catch
                {
                    Thread.Sleep(waitTimeSeconds * 1000);
                    Debug.WriteLine($"Waiting for posgress {i}/{connectionAttemptsNumber} tries.");

                    continue;
                }

                break;
            }
        }
    }
}
