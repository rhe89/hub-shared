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
            
        serviceCollection.TryAddTransient<IHubDbRepository, HubDbRepository<TDbContext>>();
        serviceCollection.TryAddTransient<ICommandConfigurationProvider, CommandConfigurationProvider>();
        serviceCollection.TryAddTransient<ICommandConfigurationFactory, CommandConfigurationFactory>();
            
        serviceCollection.AddAutoMapper(c =>
        {
            c.AddProfile<CommandConfigurationProfile>();
        });
    }
}