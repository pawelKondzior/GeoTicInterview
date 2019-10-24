using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GeoApp.BL.Contracts.DTO.Configuration;
using GeoApp.BL.Services.Init;
using GeoApp.DAL.Init;
using GeoApp.Web.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace GeoApp.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // ConfigureServices is where you register dependencies and return an `IServiceProvider` implemented by `AutofacServiceProvider`.
        // This is the old, not recommended way, and is NOT SUPPORTED in ASP.NET Core 3.0+.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Add services to the collection
            services.AddOptions();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(typeof(BLMapperProfile));
            //Mapper.AssertConfigurationIsValid();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Values Api", Version = "v1" });
            });

            // create a container-builder and register dependencies
            var builder = new ContainerBuilder();

            // populate the service-descriptors added to `IServiceCollection`
            // BEFORE you add things to Autofac so that the Autofac
            // registrations can override stuff in the `IServiceCollection`
            // as needed
            builder.Populate(services);

            builder.Register(c =>
            {
                var confDto = new HereApiConfDTO();

                Configuration.GetSection("HereApiConfDTO").Bind(confDto) ;

                return confDto;
            }).As<HereApiConfDTO>();

            builder.RegisterModule(new BLAutofacModule());
            builder.RegisterModule(new DALAutofacModule());

            AutofacContainer = builder.Build();

            // this will be used as the service-provider for the application!
            return new AutofacServiceProvider(AutofacContainer);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ///app.UseSerilogRequestLogging();

            app.UseMiddleware<SerilogMiddleware>();

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Values Api V1");
            });
        }
    }
}
