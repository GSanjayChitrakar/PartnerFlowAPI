using FGLI_SharedLibrary.Abstractions;
using FGLI_SharedLibrary.Core.Models.Configuration;
using FGLI_SharedLibrary.Infrastructure.AzureQueue;
using FGLI_SharedLibrary.Infrastructure.Configuration;
using FGLI_SharedLibrary.Infrastructure.LocalServices;
using FGLI_SharedLibrary.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PartnerFlowAPI.Api.Middleware;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Services.Configuration;
using PartnerFlowAPI.Services.Implementation;
using PartnerFlowAPI.Services.Interfaces;
using System.Text;


namespace PartnerFlowAPI.Api
{   
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddApplicationServices();
            return services;
        }

        public static IServiceCollection AddApplicationConfigurations(this IServiceCollection services, IConfigurationService configurationService)
        {
            services.Configure<AzureOptions>(configurationService.GetConfigurationSection("AzureQueue"));
            services.Configure<AzureOptions>(configurationService.GetConfigurationSection("AzureQueue"));
            services.Configure<BlobStorageSetttings>(configurationService.GetConfigurationSection("BlobStorageSetttings"));
            //services.Configure<AppSetting>(configurationService.GetConfigurationSection("AppSetting"));
            //services.Configure<FileValidationOptions>(configurationService.GetConfigurationSection("FileValidation"));



            return services;
        }
        private static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register Partner Auth Service
            services.AddScoped<IPartnerAuthService, PartnerAuthService>();
            services.AddScoped<IPartnerDataService, PartnerDataService>();
            services.AddScoped<IFieldService, FieldService>();

            // Register additional required application services
            services.AddScoped<IFileValidationService, FileValidationService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IRemoteServices, RemoteServices>();
            services.AddScoped<ILoggerService, LoggingService>();

            // Register MediatR handlers/controllers from the application assembly
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PartnerDataService).Assembly));

            return services;
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistence(configuration)
                    .AddJwtAuthentication(configuration);// Add JWT authentication
                   /* .AddRedisCache(configuration);*/ // Add Redis cache

            return services;
        }

        private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Set up Entity Framework Core with SQL Server
			services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(
					configuration.GetConnectionString("DefaultConnection"),
					sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 5,
							maxRetryDelay: TimeSpan.FromSeconds(10),
							errorNumbersToAdd: null);
					}
				));

            return services;
        }

        private static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(60),
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey ?? "")),
                    
                };
            });

            return services;
        }

        //private static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var redisConnectionString = configuration.GetConnectionString("Redis") ?? "localhost:6379";
            
        //    services.AddStackExchangeRedisCache(options =>
        //    {
        //        options.Configuration = redisConnectionString;
        //        options.InstanceName = "PartnerFlowAPI";
        //    });

        //    return services;
        //}

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            // CORS policy configuration
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            // Add Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "PartnerFlow API", 
                    Version = "v1",
                    Description = "Partner Authentication API with JWT Token Generation"
                });

                // Add JWT Bearer Security Definition
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                });

                // Add Security Requirement
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            services.AddProblemDetails();
            //services.AddOcelot();

            return services;
        }
    }
}
