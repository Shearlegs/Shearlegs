using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using Shearlegs.Web.API.Brokers.Encryptions;
using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Brokers.Validations;
using Shearlegs.Web.API.Services.Foundations.Plugins;
using Shearlegs.Web.API.Services.Foundations.Users;
using Shearlegs.Web.API.Services.Processings.Users;
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

            return services;
        }

        public static IServiceCollection AddFoundations(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPluginService, PluginService>();

            return services;
        }

        public static IServiceCollection AddProcessings(this IServiceCollection services)
        {
            services.AddTransient<IUserProcessingService, UserProcessingService>();

            return services;
        }
    }
}
