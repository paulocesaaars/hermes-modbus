using Deviot.Common;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.Data;
using Deviot.Hermes.ModbusTcp.Data.Configuration;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Data
{
    [ExcludeFromCodeCoverage]
    public class RepositoryTest
    {
        private readonly IRepository _repository;

        public RepositoryTest()
        {
            var context = CreateContext();
            context.Database.EnsureCreated();

            _repository = new Repository(context);
        }

        private static ApplicationDbContext CreateContext()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseSqlite(connection)
                                .Options;

            return new ApplicationDbContext(options);
        }

        private static User GetUserAdmin() => new User(new Guid("7011423f65144a2fb1d798dec19cf466"), "Administrador", "admin", Utils.Encript("admin"), true);

        private static User GetUserPaulo() => new User(new Guid("9b200ae920aa4898b0fd7d2d4e68eaab"), "Paulo César de Souza", "paulo", "123", true);

        [Fact(DisplayName = "Busca com filtro")]
        public async Task Get_DeveRetornarUsuarioPaulo()
        {
            var esperado = GetUserAdmin();

            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == esperado.Id);

            resultado.Should().BeEquivalentTo(esperado);
        }

        [Fact(DisplayName = "Adicionar")]
        public async Task Add_DeveRetornarUsuarioNovo()
        {
            var newUser = GetUserPaulo();

            await _repository.AddAsync<User>(newUser);

            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == newUser.Id);

            resultado.Should().BeEquivalentTo(newUser);
        }

        [Fact(DisplayName = "Editar")]
        public async Task Edit_DeveRetornarUsuarioNovo()
        {
            var userAdmin = GetUserAdmin();

            userAdmin.SetName("Usuario editado");
            await _repository.EditAsync(userAdmin);
            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == userAdmin.Id);

            resultado.Should().BeEquivalentTo(userAdmin);
        }

        [Fact(DisplayName = "Deletar")]
        public async Task Delete_DeveRetornarUsuarioNovo()
        {
            var userAdmin = GetUserAdmin();
            var total = await _repository.Get<User>().CountAsync();

            await _repository.DeleteAsync(userAdmin);
            var resultado = await _repository.Get<User>().CountAsync();

            total.Should().BeGreaterThan(resultado);
        }
    }
}