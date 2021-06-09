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
using Shearlegs.Web.Database.Repositories;
using System;
using System.Data.SqlClient;
using System.Net.Http;

namespace Shearlegs.Web
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
            services.AddTransient(x => new SqlConnection(Configuration.GetConnectionString("Default")));

            services.AddTransient<UsersRepository>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;

                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();
            services.AddAuthorizationCore();

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            services.AddHttpClient();
            services.AddScoped(s => 
            {
                var uriHelper = s.GetRequiredService<NavigationManager>();
                var httpContextAccessor = s.GetRequiredService<IHttpContextAccessor>();
                var httpClient = new HttpClient()
                {
                    BaseAddress = new Uri(uriHelper.BaseUri)
                };
                if (httpContextAccessor.HttpContext.Request.Cookies.TryGetValue(".AspNetCore.Cookies", out var cookieValue))
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("cookie", $".AspNetCore.Cookies={cookieValue}");
                }
                return httpClient;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
