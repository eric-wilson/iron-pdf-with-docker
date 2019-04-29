using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IronPdfDockerWebApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {

            var builder = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment}.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.hosting-{hostingEnvironment}.json", optional: true, reloadOnChange: true)                
                .AddEnvironmentVariables()
                //.AddCommandLine(CommandLineArgs)
                ;
            Configuration = builder.Build();


            var args = Configuration.AsEnumerable();

            // display current configurations
            foreach (var arg in args)
            {
                Console.WriteLine($"key {arg.Key} value {arg.Value}");
            }

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
