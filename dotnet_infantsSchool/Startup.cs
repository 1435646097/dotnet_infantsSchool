using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Model.Entitys;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Reflection;
using System.Text;

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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContextPool<InfantsSchoolSystemContext>(option =>
            {
                option.UseSqlServer(_configuration.GetConnectionString("default"));
                option.EnableSensitiveDataLogging();
                option.UseLazyLoadingProxies();
            });
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Email = "1435646097@qq.com",
                        Name = "排骨",
                        Url = new Uri("http://www.baidu.com")
                    },
                    Description = "一个非常简单的WEb Api",
                    Title = "千里马幼儿园后台管理系统Api"
                });
                setup.OperationFilter<AddResponseHeadersFilter>();
                setup.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                setup.OperationFilter<SecurityRequirementsOperationFilter>();
                setup.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    Description = "请输入accessToken"
                });
            });
            services.AddAuthentication("Bearer").AddJwtBearer(configure =>
            {
                configure.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Authentication:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Authentication:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SigningKey"])),
                    RequireExpirationTime = true,
                    ClockSkew = TimeSpan.FromDays(7),
                    ValidateLifetime = true
                };
            });
            services.AddCors(option =>
            {
                option.AddPolicy("default", configure =>
                {
                    configure.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    configure.WithExposedHeaders("X-Pagination");
                });
            });
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