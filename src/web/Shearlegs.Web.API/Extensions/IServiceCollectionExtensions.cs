using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Brokers.Serializations;
using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Brokers.Validations;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Services.Foundations.Versions;
using Shearlegs.Web.API.Services.Processings.Users;
using Shearlegs.Web.API.Services.Processings.Versions;
using Shearlegs.Web.API.Services.Users;

namespace Shearlegs.Web.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddBrokers(this IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<IValidationBroker, ValidationBroker>();
            services.AddTransient<IEncryptionBroker, EncryptionBroker>();
            services.AddTransient<ISerializationBroker, SerializationBroker>();

            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPluginService, PluginService>();
            services.AddTransient<IVersionService, VersionService>();

            return services;
        }

        public static IServiceCollection AddProcessings(this IServiceCollection services)
        {
            services.AddTransient<IUserProcessingService, UserProcessingService>();
            services.AddTransient<IVersionProcessingService, VersionProcessingService>();

            return services;
        }
    }
}
