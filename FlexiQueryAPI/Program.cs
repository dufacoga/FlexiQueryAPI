using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using FlexiQueryAPI.Services;
using FlexiQueryAPI.Security;

namespace FlexiQueryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Cargar configuración de base de datos
            var dbProvider = builder.Configuration["DatabaseProvider"] ?? "SqlServer";

            // Inyectar servicio de ejecución según proveedor
            builder.Services.AddScoped<ISqlExecutor>(sp =>
            {
                return dbProvider switch
                {
                    "MySQL" => new MySqlExecutor(builder.Configuration.GetConnectionString("MySQL")!),
                    "SQLite" => new SqliteExecutor(builder.Configuration.GetConnectionString("SQLite")!),
                    _ => new SqlServerExecutor(builder.Configuration.GetConnectionString("SqlServer")!)
                };
            });

            // Agrega autenticación por API Key
            builder.Services.AddSingleton<ApiKeyValidator>();
            builder.Services.AddAuthentication("ApiKeyScheme")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKeyScheme", null);

            // Controladores y Swagger
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
