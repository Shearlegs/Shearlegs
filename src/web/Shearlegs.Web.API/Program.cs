using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shearlegs.Web.API.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddBrokers();
builder.Services.AddFoundations();
builder.Services.AddProcessings();
builder.Services.AddOrchestrations();
builder.Services.AddCoordinations();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHangfireDashboard();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();