using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HomeAndEstate
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggle>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env,
            FeatureToggle features
            )
        {
            app.UseExceptionHandler("/error.html");
            if (features.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

                app.UseMvc(routes =>
              {
                  routes.MapRoute("Default", "{controller=Home}/{action=Index}/{id}");
              });

            app.UseFileServer();
        }
    }
}
