using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Common.Tools;
using dotnet_infantsSchool.Ext;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Model.Entitys;
using StackExchange.Redis;
using System;
using System.IO;
using System.Reflection;

namespace dotnet_infantsSchool
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public static ILoggerRepository repository { get; set; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            repository = LogManager.CreateRepository("InfantsSchool");
            XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            services.AddSingleton<ILoggerHelper, LogHelper>();
            services.AddHttpContextAccessor();
            services.AddControllers(setup =>
            {
                setup.Filters.Add(typeof(GlobalExceptionFilter));
            }).AddNewtonsoftJson(setup =>
            {
                setup.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddHttpContextAccessor();
            //数据库连接池配置
            services.AddDbContextPool<InfantsSchoolSystemContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("default"));
                option.UseLazyLoadingProxies();
            });
            //Swagger配置
            services.MySwagger();
            //Bearer认证配置
            services.MyAuthentication(_configuration);
            //Cors配置
            services.MyCors();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(_configuration["redisServer"]));
            services.AddScoped<IRedisHelper, RedisHelper>();
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