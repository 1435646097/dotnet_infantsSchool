using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Text;

namespace dotnet_infantsSchool.Ext
{
    public static class ServiceExt
    {
        public static IServiceCollection MySwagger(this IServiceCollection services)
        {
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
            return services;
        }

        public static IServiceCollection MyAuthentication(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddScoped<IAuthorizationHandler, ActionHandler>();
            services.AddAuthentication(configureOptions =>
            {
                configureOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                configureOptions.DefaultChallengeScheme = nameof(ApiResponseHandler);
                configureOptions.DefaultForbidScheme = nameof(ApiResponseHandler);
            }).AddJwtBearer(configure =>
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
            }).AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });
            services.AddAuthorization(configure =>
            {
                configure.AddPolicy("actionAuthrization", policy => { policy.Requirements.Add(new ActionRequirement()); });
            });
            return services;
        }

        public static IServiceCollection MyCors(this IServiceCollection services)
        {
            services.AddCors(option =>
            {
                option.AddPolicy("default", configure =>
                {
                    configure.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080", "http://47.115.23.104:80", "http://47.115.23.104", "http://172.16.153.44:8080")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                    configure.WithExposedHeaders("X-Pagination");
                });
            });
            return services;
        }
    }
}