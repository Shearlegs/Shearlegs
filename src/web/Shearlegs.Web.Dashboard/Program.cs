using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Shearlegs.Web.APIClient;
using Shearlegs.Web.Dashboard.Brokers.Cookies;
using Shearlegs.Web.Dashboard.Brokers.Loggings;
using Shearlegs.Web.Dashboard.Services;
using System;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddScoped<ICookieBroker, CookieBroker>();
builder.Services.AddScoped<ILoggingBroker, LoggingBroker>();
builder.Services.AddLogging();

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<UserState>();
builder.Services.AddScoped<AuthenticationService>();

builder.Services.AddScoped<ShearlegsWebAPIClient>(services => 
{
    IHttpContextAccessor httpContextAcccesor = services.GetRequiredService<IHttpContextAccessor>();
    IRequestCookieCollection requestCookies = httpContextAcccesor.HttpContext.Request.Cookies;
    string jwtToken = requestCookies["JWT"];

    HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:44300")
    };

    return new ShearlegsWebAPIClient(httpClient, jwtToken);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();