using Deviot.Hermes.ModbusTcp.Business.Entities;
using Deviot.Hermes.ModbusTcp.Business.Interfaces;
using Deviot.Hermes.ModbusTcp.TDD.Fakes;
using Deviot.Hermes.ModbusTcp.TDD.Helpers;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.TDD.Data
{
    public class RepositoryTest
    {
        private readonly IRepository _repository;

        public RepositoryTest()
        {
            _repository = RepositoryHelper.GetRepository();
        }


        [Fact(DisplayName = "Busca com filtro")]
        public async Task Get_DeveRetornarUsuario()
        {
            var esperado = UserFake.GetUserAdmin(true);

            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == esperado.Id);

            resultado.Should().BeEquivalentTo(esperado);
        }

        [Fact(DisplayName = "Adicionar")]
        public async Task Add_DeveRetornarUsuarioNovo()
        {
            var newUser = new User(Guid.NewGuid(), "Novo usuario", "novo", "123456");

            await _repository.AddAsync<User>(newUser);

            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == newUser.Id);

            resultado.Should().BeEquivalentTo(newUser);
        }

        [Fact(DisplayName = "Editar")]
        public async Task Edit_DeveRetornarUsuarioNovo()
        {
            var userAdmin = UserFake.GetUserAdmin();

            userAdmin.SetFullName("Usuario editado");
            await _repository.EditAsync(userAdmin);
            var resultado = await _repository.Get<User>().FirstOrDefaultAsync(u => u.Id == userAdmin.Id);

            resultado.Should().BeEquivalentTo(userAdmin);
        }

        [Fact(DisplayName = "Deletar")]
        public async Task Delete_DeveRetornarUsuarioNovo()
        {
            var userAdmin = UserFake.GetUserAdmin();
            var total = await _repository.Get<User>().CountAsync();

            await _repository.DeleteAsync(userAdmin);
            var resultado = await _repository.Get<User>().CountAsync();

            total.Should().BeGreaterThan(resultado);
        }
    }
}