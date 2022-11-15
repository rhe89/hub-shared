using Hub.Shared.HostedServices.Commands;
using Hub.Shared.Storage.Repository.Core;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Hub.Shared.Storage.Repository;

public static class ServiceCollectionExtensions
{
    [UsedImplicitly]
    public static void AddDatabase<TDbContext>(this IServiceCollection serviceCollection, IConfiguration configuration, string connectionStringKey)
        where TDbContext : HubDbContext
    {
        serviceCollection.AddDbContext<TDbContext>(options =>
            options.UseSqlServer(configuration.GetValue<string>(connectionStringKey)));
            
        serviceCollection.AddTransient<IHubDbRepository, HubDbRepository<TDbContext>>();
        serviceCollection.AddTransient<ICacheableHubDbRepository, CacheableHubDbRepository<TDbContext>>();
        serviceCollection.TryAddSingleton<ICommandConfigurationProvider, CommandConfigurationProvider>();
        serviceCollection.TryAddSingleton<ICommandConfigurationFactory, CommandConfigurationFactory>();
        serviceCollection.AddMemoryCache();

        serviceCollection.AddAutoMapper(c =>
        {
            c.AddProfile<CommandConfigurationProfile>();
        });
    }
}