﻿using Deviot.Hermes.ModbusTcp.Data;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Deviot.Hermes.ModbusTcp.TDD.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryHelper
    {
        private static ApplicationDbContext CreateContext()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;

            return new ApplicationDbContext(options);
        }

        public static Repository GetRepository()
        {
            var context = CreateContext();
            context.Database.EnsureCreated();

            return new Repository(context);
        }
    }
}
