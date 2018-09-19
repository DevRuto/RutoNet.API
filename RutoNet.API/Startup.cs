using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Npgsql;
using RutoNet.API.Interfaces.Repository;
using RutoNet.API.Repository;
using Serilog;
using SqlKata.Compilers;
using SqlKata.Execution;
using Swashbuckle.AspNetCore.Swagger;

namespace RutoNet.API
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
            //services.AddScoped<IDbConnection>(_ =>
            //    new NpgsqlConnection(Configuration.GetConnectionString("PostgresConnectionString")));

            services.AddScoped(_ => new QueryFactory(
                new NpgsqlConnection(Configuration.GetConnectionString("PostgresConnectionString")),
                new PostgresCompiler()));

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddMvc()
                .AddJsonOptions(x =>
                {
                    x.SerializerSettings.ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    };
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddTransient<IGokzRepository, GokzRepository>();

            services.AddSingleton(Log.Logger);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Ruto's API", Version = "v1" }); });
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseSwagger(c => { c.RouteTemplate = "api/swagger/{documentName}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Ruto Api V1");
                c.RoutePrefix = "api/swagger";
            });

            // using nginx as proxy
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}