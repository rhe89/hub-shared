using Hub.Shared.Storage.Repository;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Shared.Web.Api;

public class Startup<TDependencyRegistrationFactory, TDbContext>
    where TDependencyRegistrationFactory : DependencyRegistrationFactory<TDbContext>, new()
    where TDbContext : HubDbContext
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
        
    [UsedImplicitly]
    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        new TDependencyRegistrationFactory().AddServices(serviceCollection, _configuration);
    }
        
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    [UsedImplicitly]
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
            
        UpdateDatabase(app);
            
        app.UseRouting();
        app.UseCors();
            
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }

    private static void UpdateDatabase(IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<TDbContext>();
            
        context.Database.Migrate();
    }
}