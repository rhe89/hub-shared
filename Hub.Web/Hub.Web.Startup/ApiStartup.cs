using Hub.Web.DependencyRegistration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Web.Startup
{
    public abstract class ApiStartup<TDbContext, TDependencyRegistrationFactory>
        where TDbContext : DbContext
        where TDependencyRegistrationFactory : ApiWithQueueHostedServiceDependencyRegistrationFactory<TDbContext>, new()
    {
        private readonly IConfiguration _configuration;

        protected ApiStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            new TDependencyRegistrationFactory().BuildServiceCollection(serviceCollection, _configuration);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            UpdateDatabase(app);
            
            app.UseRouting();

            // app.UseAuthorization();

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
}