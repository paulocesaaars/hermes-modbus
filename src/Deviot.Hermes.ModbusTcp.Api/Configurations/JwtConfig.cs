﻿using Deviot.Hermes.ModbusTcp.Business.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Deviot.Hermes.ModbusTcp.Api.Configurations
{
    public static class JwtConfig
    {
        private const string JWT_SETTINGS = nameof(JwtSettings);

        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT Configurations
            var config = configuration.GetSection(JWT_SETTINGS);
            services.Configure<JwtSettings>(config);
            var jwtSettings = config.Get<JwtSettings>();

            var key = Encoding.ASCII.GetBytes(jwtSettings.Key);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.ValidAudience,
                    ValidIssuer = jwtSettings.ValidIssuer
                };
            });

            return services;
        }

        public static IApplicationBuilder UseJwtConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();

            app.UseAuthorization();

            return app;
        }
    }
}
