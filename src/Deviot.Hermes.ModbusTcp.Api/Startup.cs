using Deviot.Hermes.ModbusTcp.Api.Configurations;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Api
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Dependency injection config
            services.AddDependencyInjectionConfiguration(Configuration);

            // Api - Configuration
            services.AddApiConfiguration(Configuration);

            // Versionamento
            services.AddVersioningConfiguration();

            // Swagger config
            services.AddSwaggerConfiguration();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationService databaseService)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(env);

            if (env.IsDevelopment())
                databaseService.DeleteDatabase();

            databaseService.Execute();
        }
    }
}
