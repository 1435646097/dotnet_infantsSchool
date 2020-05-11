using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Model.Entitys;
using System;
using System.IO;
using System.Reflection;

namespace dotnet_infantsSchool
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddDbContextPool<InfantsSchoolSystemContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("default"));
                option.EnableSensitiveDataLogging();
                option.UseLazyLoadingProxies();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            string basePath = AppContext.BaseDirectory;
            Assembly servicesAssembly = Assembly.LoadFrom(Path.Combine(basePath, "Services.Dll"));
            Assembly repositoryAssembly = Assembly.LoadFrom(Path.Combine(basePath, "Repository.Dll"));
            containerBuilder.RegisterAssemblyTypes(servicesAssembly)
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope()
                            .EnableClassInterceptors();
            containerBuilder.RegisterAssemblyTypes(repositoryAssembly)
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope()
                            .EnableClassInterceptors();
        }
    }
}