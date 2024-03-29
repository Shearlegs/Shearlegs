using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shearlegs.Runtime;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Services;
using System;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shearlegs.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(x => new SqlConnection(Configuration.GetConnectionString("Default")));

            services.AddTransient<UsersRepository>();
            services.AddTransient<PluginsRepository>();
            services.AddTransient<VersionsRepository>();
            services.AddTransient<ResultsRepository>();

            ShearlegsRuntime.RegisterServices(services);
            services.AddTransient<PluginService>();
            services.AddScoped<UserService>();
            services.AddTransient<ConfigService>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;

                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddRazorPages();
            services.AddControllers();
            IServerSideBlazorBuilder serverSideBlazorBuilder = services.AddServerSideBlazor();
            
            if (Environment.IsDevelopment())
            {
                serverSideBlazorBuilder.AddCircuitOptions(options =>
                {
                    options.DetailedErrors = true;
                });
            }

            services.AddAuthorization();
            services.AddAuthorizationCore();

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped<HttpClient>();
            services.AddSignalR(e => 
            {
                e.MaximumReceiveMessageSize = 102400000;
            } );
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
