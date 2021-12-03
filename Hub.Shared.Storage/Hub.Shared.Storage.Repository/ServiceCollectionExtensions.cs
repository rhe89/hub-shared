using AutoMapper;
using Hub.Shared.HostedServices.Commands;
using Hub.Shared.Settings;
using Hub.Shared.Storage.Repository.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.Storage.Repository
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabase<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey, string migrationAssembly)
            where TDbContext : HubDbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey), 
                    x => x.MigrationsAssembly(migrationAssembly)));
            
            serviceCollection.TryAddTransient<IHubDbRepository, HubDbRepository<TDbContext>>();
            serviceCollection.TryAddTransient<ICommandConfigurationProvider, CommandConfigurationProvider>();
            serviceCollection.TryAddTransient<ICommandConfigurationFactory, CommandConfigurationFactory>();
            serviceCollection.TryAddTransient<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddTransient<ISettingProvider, SettingProvider>();
            
            serviceCollection.AddAutoMapper(c =>
            {
                c.AddProfile<CommandConfigurationProfile>();
                c.AddProfile<SettingMapperProfile>();
            });
        }
        
        public static void AddDatabase<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey)
            where TDbContext : HubDbContext
        {
            serviceCollection.AddDbContext<TDbContext>(options => 
                options.UseSqlServer(configuration.GetValue<string>(connectionStringKey)));
            
            serviceCollection.TryAddTransient<IHubDbRepository, HubDbRepository<TDbContext>>();
            serviceCollection.TryAddTransient<ICommandConfigurationProvider, CommandConfigurationProvider>();
            serviceCollection.TryAddTransient<ICommandConfigurationFactory, CommandConfigurationFactory>();
            serviceCollection.TryAddTransient<ISettingFactory, SettingFactory>();
            serviceCollection.TryAddTransient<ISettingProvider, SettingProvider>();
            
            serviceCollection.AddAutoMapper(c =>
            {
                c.AddProfile<CommandConfigurationProfile>();
                c.AddProfile<SettingMapperProfile>();
            });
        }
    }
}