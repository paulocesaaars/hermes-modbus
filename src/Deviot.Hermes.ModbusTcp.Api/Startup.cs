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

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment environment, IMigrationService migrationService)
        {
            app.UseSwaggerConfiguration();

            app.UseApiConfiguration(environment);

            if (environment.EnvironmentName == "Development")
                migrationService.Deleted();

            migrationService.Execute();

            if (environment.EnvironmentName == "Testing")
                migrationService.Populate();
        }
    }
}
