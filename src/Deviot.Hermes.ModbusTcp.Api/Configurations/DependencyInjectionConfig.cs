using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Business.Services;
using Deviot.Hermes.ModbusTcp.Business.Validators;
using Deviot.Hermes.ModbusTcp.Data;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Deviot.Hermes.ModbusTcp.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        private static string CONNECTION_STRING = "SQLiteConnection";

        private static string CONNECTION_STRING_ERROR = "A conexão do SQLite não foi informada.";

        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection String
            var sqliteConnection = configuration.GetConnectionString(CONNECTION_STRING);
            if (string.IsNullOrEmpty(sqliteConnection)) throw new ArgumentNullException(CONNECTION_STRING_ERROR);

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlite(sqliteConnection));

            services.AddScoped<INotifier, Notifier>();

            // Validations
            services.AddScoped<IValidator<Login>>(v => new LoginValidator());
            services.AddScoped<IValidator<User>>(v => new UserValidator());

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IMigrationService, MigrationService>();

            return services;
        }
    }
}
