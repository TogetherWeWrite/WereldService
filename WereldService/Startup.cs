using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WereldService.Helpers;
using WereldService.MessageHandlers;
using WereldService.Publishers;
using WereldService.Repositories;
using WereldService.Services;
using WereldService.Settings;
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
            #region jwt
            var appSettingsSection = Configuration.GetSection("jwt");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });
            #endregion
            services.Configure<WereldstoreDatabaseSettings>(
                Configuration.GetSection(nameof(WereldstoreDatabaseSettings)));

            services.AddSingleton<IWereldstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<WereldstoreDatabaseSettings>>().Value);
            #region mq
            
            services.Configure<MessageQueueSettings>(Configuration.GetSection("MessageQueueSettings"));
            services.AddMessagePublisher(Configuration["MessageQueueSettings:Uri"]);
            services.AddMessageConsumer(Configuration["MessageQueueSettings:Uri"],
                "authentication-service",
                builder => builder.WithHandler<UserMessageHandler>("register-new-user"));
            services.AddTransient<IWorldPublisher, WorldPublisher>();
            
            #endregion

            #region repos
            
            services.AddTransient<IWorldRepository, WorldRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            
            #endregion

            #region helper
            
            services.AddTransient<IAuthenticationHelper, AuthenticationHelper>();

            #endregion

            #region services

            services.AddTransient<IWorldFollowService, WorldFollowService>();
            services.AddTransient<IWorldManagementService, WorldManagementService>();
            services.AddTransient<IWorldOverviewService, WorldOverviewService>();
            services.AddTransient<IWorldUserManagementService, WorldManagementService>();

            #endregion




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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
           
        }
    }
}
