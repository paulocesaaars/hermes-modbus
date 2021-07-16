using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Api.Mappings;
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
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Api.Configurations
{
    [ExcludeFromCodeCoverage]

    public static class DependencyInjectionConfig
    {
        private static string CONNECTION_STRING = "SQLiteConnection";

        private static string CONNECTION_STRING_ERROR = "A conexão do SQLite não foi informada";

        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Commons
            services.AddScoped<INotifier, Notifier>();

            // Automapper
            services.AddAutoMapper(typeof(EntityToViewModelMapping),
                                   typeof(ViewModelToEntityMapping));

            // Validations
            services.AddScoped<IValidator<Login>>(v => new LoginValidator());
            services.AddScoped<IValidator<User>>(v => new UserValidator());
            services.AddScoped<IValidator<UserInfo>>(v => new UserInfoValidator());
            services.AddScoped<IValidator<UserPassword>>(v => new UserPasswordValidator());

            // Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();

            // Connection String
            var sqliteConnection = configuration.GetConnectionString(CONNECTION_STRING);
            if (string.IsNullOrEmpty(sqliteConnection))
                throw new ArgumentNullException(CONNECTION_STRING_ERROR);

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlite(sqliteConnection));

            // Data
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IMigrationService, MigrationService>();

            return services;
        }
    }
}
