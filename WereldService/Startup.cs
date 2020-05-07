using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WereldService.Helpers;
using WereldService.Repositories;
using WereldService.Services;
using WereldService.WereldStoreDatabaseSettings;
using WereldService.WereldStoreDatabaseSettings.authenticationservice.DatastoreSettings;

namespace WereldService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            
            services.Configure<WereldstoreDatabaseSettings>(
                Configuration.GetSection(nameof(WereldstoreDatabaseSettings)));

            services.AddSingleton<IWereldstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WereldstoreDatabaseSettings>>().Value);

            services.AddTransient<IWorldRepository, WorldRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.AddTransient<IUserHelper, UserHelper>();

            services.AddTransient<IWorldFollowService, WorldFollowService>();
            services.AddTransient<IWorldManagementService, WorldManagementService>();
            services.AddTransient<IWorldOverviewService, WorldOverviewService>();
            services.AddTransient<IWorldUserManagementService, WorldManagementService>();
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseCors(x => x
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
