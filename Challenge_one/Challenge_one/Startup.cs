using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Challenge_one.Aplication;
using Challenge_one.Aplication.Extensions;
using Challenge_one.Aplication.SlotQueue;
using Challenge_one.Infrastructure;
using Challenge_one.MsSql.SlotRepository;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Core.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Challenge_one
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
            services.AddControllers();

            services.AddApplicationDependencies(Configuration, true);

            services.AddApiVersioning(
               options =>
               {
                   // reporting api versions will return the headers "api-supported-versions" and "api-deprecated-versions"
                   options.ReportApiVersions = true;
               });

            services.AddVersionedApiExplorer(
                options =>
                {
                    // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                    // note: the specified format code will format the version as "'v'major[.minor][-status]"
                    options.GroupNameFormat = "'v'VVV";

                    // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                    // can also be used to control the format of the API version in route templates
                    options.SubstituteApiVersionInUrl = true;

                });

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // add a custom operation filter which sets default values
                    options.OperationFilter<SwaggerDefaultValues>();

                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use(RedirectToSwagger);
            app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/swagger.json");
            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "docs";
                    // build a swagger endpoint for each discovered API version
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/docs/{description.GroupName}/swagger.json",
                            description.GroupName.ToUpperInvariant());
                    }
                });
        }

        private static async Task RedirectToSwagger(HttpContext context, Func<Task> next)
        {
            // Redirect requests without a path to the default swagger UI page.
            if (context.Request.Path.Value == "/")
            {
                context.Response.Redirect($"/docs");
                return;
            }

            await next();
        }
    }
}
