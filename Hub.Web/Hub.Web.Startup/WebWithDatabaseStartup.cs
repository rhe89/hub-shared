using Hub.Web.DependencyRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hub.Web.Startup
{
    public abstract class WebWithDatabaseStartup<TDependencyRegistrationFactory, TDbContext> : WebStartup<TDependencyRegistrationFactory>
        where TDependencyRegistrationFactory : WebDependencyRegistrationFactory, new()
        where TDbContext : DbContext
    {
        protected WebWithDatabaseStartup(IConfiguration configuration) : base(configuration) { }
        
        protected new void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            base.ConfigureApp(app, env);
            
            UpdateDatabase(app);
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
}