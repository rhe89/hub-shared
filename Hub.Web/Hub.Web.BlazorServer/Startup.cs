using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hub.Web.BlazorServer
{
    public class Startup<TDependencyRegistrationFactory> 
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase, new()
    {
        private readonly IConfiguration _configuration;

        protected Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            new TDependencyRegistrationFactory().BuildServiceCollection(services, _configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureApp(app, env);
        }

        protected void ConfigureApp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
    
    public class Startup<TDependencyRegistrationFactory, TDbContext> : Startup<TDependencyRegistrationFactory>
        where TDependencyRegistrationFactory : DependencyRegistrationFactoryBase, new()
        where TDbContext : DbContext
    {
        protected Startup(IConfiguration configuration) : base(configuration) { }
        
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