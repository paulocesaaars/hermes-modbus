using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using Deviot.Hermes.ModbusTcp.Data.TestData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Deviot.Hermes.ModbusTcp.Data
{
    [ExcludeFromCodeCoverage]
    public class MigrationService : IMigrationService
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        private readonly ApplicationDbContext _applicationDbContext;

        private const string MIGRATION_EXECUTE_ERROR = "Não foi possível executar a migration";
        private const string MIGRATION_DELETED_ERROR = "Não foi possível deletar o banco de dados";
        private const string MIGRATION_POPULATE_ERROR = "Não foi possível popular o banco de dados";


        public MigrationService(ApplicationDbContext applicationDbContext, ILogger<MigrationService> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _applicationDbContext = applicationDbContext;
        }

        private async Task PopulateUsersAsync()
        {
            var users = await _repository.Get<User>().ToListAsync();
            var userDataList = UserData.GetUsers();

            // Expurge
            foreach (var user in users)
            {
                if (!userDataList.Any(x => x.Id == user.Id))
                    await _repository.DeleteAsync<User>(user);
            }

            foreach (var userData in userDataList)
            {
                if (users.Any(x => x.Id == userData.Id))
                {
                    var user = users.First(x => x.Id == userData.Id);
                    user.SetFullName(userData.FullName);
                    user.SetUserName(userData.UserName);
                    user.SetPassword(userData.Password);
                    user.SetEnabled(userData.Enabled);
                    user.SetAdministrator(userData.Administrator);

                    await _repository.EditAsync<User>(user);
                }
                else
                {
                    await _repository.AddAsync<User>(userData);
                }
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

        public void Deleted()
        {
            try
            {
                _applicationDbContext.Database.EnsureDeleted();
            }
            catch (Exception exception)
            {
                _logger.LogError(MIGRATION_DELETED_ERROR);
                _logger.LogError(exception.Message);
            }
            
        }

        public void Populate()
        {
            try
            {
                var tasks = new List<Task>();
                tasks.Add(PopulateUsersAsync());
                var task = Task.WhenAll(tasks);
                task.Wait();
            }
            catch (Exception exception)
            {
                _logger.LogError(MIGRATION_POPULATE_ERROR);
                _logger.LogError(exception.Message);
            }
        }
    }
}
