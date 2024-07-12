using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Identity;
using MongoAuthWebApi.MongoDb.Configuration;
using MongoAuthWebApi.MongoDb.Identity;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Filters;

namespace Microsoft.Extensions.DependencyInjection;

public static class DepedencyInjection
{
    public static IServiceCollection AddMyMongoDbService(this IServiceCollection services, IConfiguration configuration)
    {
        // MongoDB Defaults
        BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));

        services.Configure<MongoDbConfig>(configuration.GetSection(MongoDbConfig.ConfigSection));
        var mongoDbConfig = configuration.GetSection(MongoDbConfig.ConfigSection).Get<MongoDbConfig>();

        ArgumentNullException.ThrowIfNull(mongoDbConfig);

        var connectionStringFromEnv = Environment.GetEnvironmentVariable("MongoDb__ConnectionString");
        if (!string.IsNullOrEmpty(connectionStringFromEnv))
        {
            mongoDbConfig.ConnectionString = connectionStringFromEnv;
        }

        var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
        {
            MongoDbSettings = new MongoDbSettings
            {
                ConnectionString = mongoDbConfig.ConnectionString,
                DatabaseName = mongoDbConfig.DatabaseName
            },
            IdentityOptionsAction = options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
                // Lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
            }
        };

        services.ConfigureMongoDbIdentity<MongoUser, MongoIdentityRole, Guid>(mongoDbIdentityConfig)
            .AddUserManager<UserManager<MongoUser>>()
            .AddDefaultTokenProviders();

        return services;
    }

    public static IServiceCollection AddMyJwtAuthenticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection(JwtConfig.ConfigSection));
        var jwtConfig = configuration.GetSection(JwtConfig.ConfigSection).Get<JwtConfig>();

		ArgumentNullException.ThrowIfNull(jwtConfig);

		services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                    ClockSkew = TimeSpan.FromSeconds(5)
                };
            });

        return services;
    }

    public static IServiceCollection AddApiSwaggerServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme. Example: 'bearer {token}'",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });

        return services;
    }
}