using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechShed.WebUI.AppCode.Providers;
using TechShed.WebUI.Models.DataContexts;

namespace TechShed.WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews(cfg =>
            {
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());
            });
            services.AddRouting((cfg) =>
            {
                cfg.LowercaseUrls = true;
            });

            services.AddDbContext<TechShedDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("Default"));
            });





            services.AddMediatR(this.GetType().Assembly);
            services.AddTransient<IActionContextAccessor, ActionContextAccessor>();

            services.AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblies(new[] { this.GetType().Assembly });
            });
          

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.InitDb();
           
            app.UseStaticFiles();



            

            app.UseEndpoints(cfg =>
            {
                cfg.MapAreaControllerRoute(
                    name:"Ulvi",
                    areaName:"Admin",
                    pattern: "Admin/{controller=dashboard}/{action=index}/{id?}"
                );


                cfg.MapControllerRoute("default", pattern:"{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
