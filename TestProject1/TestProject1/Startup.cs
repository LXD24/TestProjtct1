using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProject1.Filters;
using Serilog.Context;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace TestProject1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(Configuration)
            //    .MinimumLevel.Warning()
            //    .Enrich.FromLogContext()
            //    .WriteTo.Console(new RenderedCompactJsonFormatter())
            //    .CreateLogger();

            services.AddControllers(x =>
            {
                x.Filters.Add(typeof(ResultFilter));
                //x.Filters.Add(typeof(ExceptionFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<GlobalErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
