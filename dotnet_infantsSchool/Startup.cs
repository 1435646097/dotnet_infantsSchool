using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using dotnet_infantsSchool.Ext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Entitys;
using System;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Unicode;

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
            }).AddXmlSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            //数据库连接池配置
            services.AddDbContextPool<InfantsSchoolSystemContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("default"));
                option.EnableSensitiveDataLogging();
                option.UseLazyLoadingProxies();
            });
            //Swagger配置
            services.MySwagger();
            //Bearer认证配置
            services.MyAuthentication(_configuration);
            //Cors配置
            services.MyCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("swagger/v1/swagger.json", "v1");
                setup.RoutePrefix = "";
            });
            app.UseRouting();
            app.UseCors("default");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            string basePath = AppContext.BaseDirectory;
            Assembly servicesAssembly = Assembly.LoadFrom(Path.Combine(basePath, "Services.dll"));
            Assembly repositoryAssembly = Assembly.LoadFrom(Path.Combine(basePath, "Repository.dll"));
            //注入Services程序集
            containerBuilder.RegisterAssemblyTypes(servicesAssembly)
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope()
                            .EnableClassInterceptors();
            //注入Repository程序集
            containerBuilder.RegisterAssemblyTypes(repositoryAssembly)
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope()
                            .EnableClassInterceptors();
        }
    }
}