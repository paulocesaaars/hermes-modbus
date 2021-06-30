﻿using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Business.Settings
{
    [ExcludeFromCodeCoverage]
    public class JwtSettings
    {
        public string Key { get; set; }

        public double ExpirationTimeSeconds { get; set; }

        public string ValidIssuer { get; set; }

        public string ValidAudience { get; set; }
    }
}
