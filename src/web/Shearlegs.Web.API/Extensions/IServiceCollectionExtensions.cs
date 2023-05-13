using Hangfire;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Shearlegs.Runtime;
using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Brokers.HttpContexts;
using Shearlegs.Web.API.Brokers.JWTs;
using Shearlegs.Web.API.Brokers.NodeClients;
using Shearlegs.Web.API.Brokers.Schedulings;
using Shearlegs.Web.API.Brokers.Serializations;
using Shearlegs.Web.API.Brokers.Shearlegs;
using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Brokers.Validations;
using Shearlegs.Web.API.Models.Options;
using Shearlegs.Web.API.Services.Coordinations.NodeUserAuthentications;
using Shearlegs.Web.API.Services.Coordinations.NodeVariableUserAuthentications;
using Shearlegs.Web.API.Services.Coordinations.VersionUploads;
using Shearlegs.Web.API.Services.Coordinations.VersionUploadUserAuthentications;
using Shearlegs.Web.API.Services.Foundations.HttpUsers;
using Shearlegs.Web.API.Services.Foundations.JWTs;
using Shearlegs.Web.API.Services.Foundations.NodeDaemons;
using Shearlegs.Web.API.Services.Foundations.Nodes;
using Shearlegs.Web.API.Services.Foundations.NodeVariables;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.PluginSecrets;
using Shearlegs.Web.API.Services.Foundations.Results;
using Shearlegs.Web.API.Services.Foundations.Schedulings;
using Shearlegs.Web.API.Services.Foundations.ShearlegsFrameworks;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Services.Foundations.UserSessions;
using Shearlegs.Web.API.Services.Foundations.Versions;
using Shearlegs.Web.API.Services.Foundations.VersionUploads;
using Shearlegs.Web.API.Services.Orchestrations.Nodes;
using Shearlegs.Web.API.Services.Orchestrations.NodeVariables;
using Shearlegs.Web.API.Services.Orchestrations.Schedulings;
using Shearlegs.Web.API.Services.Orchestrations.UserAuthentications;
using Shearlegs.Web.API.Services.Orchestrations.Versions;
using Shearlegs.Web.API.Services.Orchestrations.VersionUploads;
using Shearlegs.Web.API.Services.Processings.Results;
using Shearlegs.Web.API.Services.Processings.Users;
using Shearlegs.Web.API.Services.Processings.Versions;
using Shearlegs.Web.API.Services.Users;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Please insert JWT with Bearer into field",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                OpenApiSecurityScheme securityScheme = new()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                OpenApiSecurityRequirement securityRequirement = new()
                {
                    { securityScheme, Array.Empty<string>() }
                };

                options.AddSecurityRequirement(securityRequirement);
            });

            // Shearlegs runtime
            ShearlegsRuntime.RegisterServices(services);

            // Hangfire
            services.AddHangfire(x =>
            {
                x.UseSqlServerStorage(configuration.GetConnectionString("Default"));
            });
            services.AddHangfireServer();            

            // Options
            services.Configure<JWTOptions>(configuration.GetSection(JWTOptions.SectionKey));

            return services;
        }

        public static IServiceCollection AddBrokers(this IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<IValidationBroker, ValidationBroker>();
            services.AddTransient<IEncryptionBroker, EncryptionBroker>();
            services.AddTransient<ISerializationBroker, SerializationBroker>();
            services.AddTransient<IShearlegsFrameworkBroker, ShearlegsFrameworkBroker>();
            services.AddTransient<ISchedulingBroker, SchedulingBroker>();
            services.AddTransient<IHttpContextBroker, HttpContextBroker>();
            services.AddTransient<IJWTBroker, JWTBroker>();
            services.AddTransient<INodeClientBroker, NodeClientBroker>();

            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPluginService, PluginService>();
            services.AddTransient<IVersionService, VersionService>();
            services.AddTransient<IShearlegsFrameworkService, ShearlegsFrameworkService>();
            services.AddTransient<IPluginSecretService, PluginSecretService>();
            services.AddTransient<IResultService, ResultService>();
            services.AddTransient<ISchedulingService, SchedulingService>();
            services.AddTransient<IUserSessionService, UserSessionService>();
            services.AddTransient<IHttpUserService, HttpUserService>();
            services.AddTransient<IJWTService, JWTService>();
            services.AddTransient<INodeService, NodeService>();
            services.AddTransient<INodeVariableService, NodeVariableService>();
            services.AddTransient<INodeDaemonService, NodeDaemonService>();
            services.AddTransient<IVersionUploadService, VersionUploadService>();

            return services;
        }

        public static IServiceCollection AddProcessings(this IServiceCollection services)
        {
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IVersionProcessingService, VersionProcessingService>();
            services.AddTransient<IResultProcessingService, ResultProcessingService>();

            return services;
        }

        public static IServiceCollection AddOrchestrations(this IServiceCollection services)
        {
            services.AddTransient<IVersionOrchestrationService, VersionOrchestrationService>();
            services.AddTransient<IUserAuthenticationOrchestrationService, UserAuthenticationOrchestrationService>();
            services.AddTransient<INodeOrchestrationService, NodeOrchestrationService>();
            services.AddTransient<INodeVariableOrchestrationService, NodeVariableOrchestrationService>();
            services.AddTransient<IVersionUploadOrchestrationService, VersionUploadOrchestrationService>();
            services.AddTransient<ISchedulingOrchestrationService, SchedulingOrchestrationService>();

            return services;
        }

        public static IServiceCollection AddCoordinations(this IServiceCollection services) 
        {
            services.AddTransient<INodeUserAuthenticationCoordinationService, NodeUserAuthenticationCoordinationService>();
            services.AddTransient<INodeVariableUserAuthenticationCoordinationService, NodeVariableUserAuthenticationCoordinationService>();
            services.AddTransient<IVersionUploadUserAuthenticationCoordinationService, VersionUploadUserAuthenticationCoordinationService>();
            services.AddTransient<IVersionUploadCoordinationService, VersionUploadCoordinationService>();

            return services;
        }
    }
}
