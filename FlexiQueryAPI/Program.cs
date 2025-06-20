using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using FlexiQueryAPI.Services;
using FlexiQueryAPI.Security;
using FlexiQueryAPI.Config;

namespace FlexiQueryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var dbOptions = builder.Configuration.Get<DbProviderOptions>()!;
            builder.Services.AddSingleton(dbOptions);

            builder.Services.AddScoped<ISqlExecutor>(sp =>
            {
                return dbOptions.DatabaseProvider switch
                {
                    "MySQL" => new MySqlExecutor(dbOptions.ConnectionStrings.MySQL),
                    "SQLite" => new SqliteExecutor(dbOptions.ConnectionStrings.SQLite),
                    _ => new SqlServerExecutor(dbOptions.ConnectionStrings.SqlServer)
                };
            });

            builder.Services.AddSingleton<ApiKeyValidator>();
            builder.Services.AddAuthentication("ApiKeyScheme")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyScheme", null);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SecureQueryAPI", Version = "v1" });
                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "X-API-KEY",
                    Type = SecuritySchemeType.ApiKey,
                    Description = "API Key Authentication"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}
