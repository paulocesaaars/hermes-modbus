using Deviot.Hermes.ModbusTcp.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.Data.Configuration
{
    [ExcludeFromCodeCoverage]
    public static class Seed
    {
        public static void Create(ModelBuilder modelBuilder)
        {
            // User
            var users = new List<User>();
            users.Add(new User(new Guid("7011423f65144a2fb1d798dec19cf466"), "Administrador", "admin", "admin", true));
            modelBuilder.Entity<User>().HasData(users);
        }
    }
}
