using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Deviot.Hermes.ModbusTcp.Data
{
    public class MigrationService : IMigrationService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ILogger _logger;

        private const string MIGRATION_EXECUTE_ERROR = "Não foi possível executar a migration";
        private const string MIGRATION_DELETE_ERROR = "Não foi possível excluir a base de dados";

        public MigrationService(ApplicationDbContext applicationDbContext, ILogger<MigrationService> logger)
        {
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }

        public void DeleteDatabase()
        {
            try
            {
                _applicationDbContext.Database.EnsureDeleted();
            }
            catch (Exception exception)
            {
                _logger.LogError(MIGRATION_DELETE_ERROR);
                _logger.LogError(exception.Message);
            }
        }

        public void Execute()
        {
            try
            {
                _applicationDbContext.Database.Migrate();
            }
            catch (Exception exception)
            {
                _logger.LogError(MIGRATION_EXECUTE_ERROR);
                _logger.LogError(exception.Message);
            }
        }
    }
}
