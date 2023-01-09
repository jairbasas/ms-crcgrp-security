using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Security.Api.Infrastructure.Filters;
using Security.Application.Wrappers;
using System.Text;

namespace Security.Api.Infrastructure.Extensions
{
    public static class IServiceCollectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddCustomMvc(this IServiceCollection services) =>
            services.AddMvcCore(options =>
            {
                options.Filters.Add(typeof(ValidateModelStateAttribute));
            })
            .AddAuthorization()
            .AddApiExplorer()
            .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            })
            .AddFluentValidation()
            .AddControllersAsServices()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .Services;

        public static IServiceCollection AddConfigureCors(this IServiceCollection services) =>
            services.AddCors(p => p.AddPolicy("corsApp", builder => {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            }));

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration) =>

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o => {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["Jwt:Issuer"].ToString(),
                    ValidAudience = configuration["Jwt:Audience"].ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"].ToString()))
                };
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        return c.Response.WriteAsync(c.Exception.Message.ToString());
                    },
                    OnChallenge = context =>
                    {
                        context.HandleResponse();
                        if (!context.Response.HasStarted) 
                        {
                            context.Response.StatusCode = 400;
                            context.Response.ContentType = "application/json";
                        }                        
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no está autorizado"));
                        return context.Response.WriteAsync(result);
                    },
                    OnForbidden = context =>
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new Response<string>("Usted no tiene permisos sobre este recurso"));
                        return context.Response.WriteAsync(result);
                    }
                };
            })
            .Services;

        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = configuration["Microservice"],
                    Version = "v1",
                    Description = "Micro Servicio de seguridad",
                    Contact = new OpenApiContact
                    {
                        Name = "Jair A. Basas",
                        Email = "antoniobasas@gmail.com"
                    }
                });
                //c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                //{
                //    Type = "oauth2",
                //    Flow = "application",
                //    TokenUrl = $"{configuration["Server:Authority"]}",
                //    Scopes = new Dictionary<string, string>()
                //    {
                //        {configuration["Server:ApiResource"], string.Empty}
                //    }
                //});
                //c.OperationFilter<IdsrvOperationFilter>();
                //c.OperationFilter<NameOperationFilter>();
                c.CustomSchemaIds((type) => SetCustomSchemaIdSelector(type));
                //c.CustomSchemaIds(i => i.FullName);
            });
            return services;
        }
        private static string SetCustomSchemaIdSelector(Type modelType)
        {
            if (!modelType.IsConstructedGenericType) return modelType.Name;

            var prefix = modelType.GetGenericArguments()
                .Select(genericArg => SetCustomSchemaIdSelector(genericArg))
                .Aggregate((previous, current) => previous + current);

            return prefix + modelType.Name.Split('`').First();
        }
    }
}
