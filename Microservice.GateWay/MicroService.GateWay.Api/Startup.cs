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
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MicroService.GateWay.Api
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
            services.AddOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("http://localhost:5001/swagger/v1/swagger.json", "ProductApi");
                c.SwaggerEndpoint("http://localhost:5002/swagger/v1/swagger.json", "OrderApi");
                c.SwaggerEndpoint("http://localhost:5003/swagger/v1/swagger.json", "CartApi");
       
            });
            await app.UseOcelot();
        }
    }
}
