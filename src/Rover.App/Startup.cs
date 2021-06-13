using Autofac;
using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rover.Domain.Models;
using Rover.Domain.Service;
using Rover.Infra;
using System;
using System.IO;
using System.Reflection;

namespace Rover.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pluto Rover",
                    Description = "An API that allows the Rover to move around a planet",
                    Contact = new OpenApiContact
                    {
                        Name = "Youcef Merzoug",
                    },
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddHealthChecks()
                .AddCheck(
                    "self",
                    () => HealthCheckResult.Healthy());

            services.AddCors(options =>
            {
                options.AddPolicy("AnyOrigin", builder =>
                {
                    builder.AllowAnyHeader()
                        .AllowAnyOrigin()
                        .AllowAnyMethod();
                });
            });

        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // TOOD: Move DI to the Rover.Domain project
            // TODO: load grid config from config files or any any other source
            builder.RegisterInstance(new GridConfiguration(100, 100)).AsImplementedInterfaces();

            builder.RegisterType<InMemoryObstacleRepository>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<InMemoryRoverRepository>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ObstacleDetector>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<RoverEngine>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<RoverQuery>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ObstacleQuery> ().Named<IQuery<bool, Coordinates>>("handler").SingleInstance();
            builder.RegisterDecorator<IQuery<bool, Coordinates>>((c, inner) => new CachedObstacleQuery(inner), fromKey: "handler").SingleInstance();

            builder.RegisterType<RoverService>().AsImplementedInterfaces();

            // Automapper config
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(Startup).GetTypeInfo().Assembly);
            });
            var mapper = mapperConfig.CreateMapper();
            builder.RegisterInstance(mapper).SingleInstance();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseCors("AnyOrigin");

                app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "rover api v1"));
            }
            else
            {
                app.UseExceptionHandler("/error");
                app.UseHsts();
            }


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
                {
                    Predicate = r => r.Name.Contains("self")
                });
            });
        }
    }
}
