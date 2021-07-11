using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fakes;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.EditarUsuario
{
    [Binding]
    [Scope(Feature = "Editar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class EditarUsuarioSteps : IntegrationTestBase
    {
        private UserInfoViewModel _user;
        private GenericActionResult<UserInfoViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public EditarUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                  ITestOutputHelper testOutputHelper
                                 ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {
        }

        [Given(@"que tenho um token de acesso admin")]
        public async Task DadoQueTenhoUmTokenDeAcessoAdmin()
        {
            var token = await GetTokenAsync(GetAdminLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [Given(@"que tenho um usuário atualizado diferente do meu")]
        public void DadoQueTenhoUmUsuarioAtualizadoDiferenteDoMeu()
        {
            _user = UserInfoFake.GetUserBruna();
            _user.FullName = "Bruna Stefano de Souza";
        }
        
        [Given(@"que tenho um token de acesso normal")]
        public async Task DadoQueTenhoUmTokenDeAcessoNormal()
        {
            var token = await GetTokenAsync(GetPauloLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [Given(@"que tenho meu usuário atualizado")]
        public void DadoQueTenhoMeuUsuarioAtualizado()
        {
            _user = UserInfoFake.GetUserPaulo();
            _user.FullName = "Paulo César";
        }
        
        [Given(@"que que tenho um usuário com id inválido")]
        public void DadoQueQueTenhoUmUsuarioComIdInvalido()
        {
            _user = new UserInfoViewModel
            {
                Id = Guid.NewGuid(),
                FullName = "Novo usuário",
                UserName = "novo_usuario",
                Administrator = false,
                Enabled = true
            };
        }
        
        [Given(@"que tenho um usuário com nome inválido")]
        public void DadoQueTenhoUmUsuarioComNomeInvalido()
        {
            _user = UserInfoFake.GetUserPaulo();
            _user.UserName = "paulo cesar";
        }
        
        [Given(@"que tenho um usuário com nome já existente")]
        public void DadoQueTenhoUmUsuarioComNomeJaExistente()
        {
            _user = UserInfoFake.GetUserPaulo();
            _user.UserName = "bruna";
        }
        
        [Given(@"que tenho meu usuário atualizado para administrador")]
        public void DadoQueTenhoMeuUsuarioAtualizadoParaAdministrador()
        {
            _user = UserInfoFake.GetUserPaulo();
            _user.Administrator = true;
        }

        [Given(@"que tenho um usuário atualizado como Administrador diferente do meu")]
        public void DadoQueTenhoUmUsuarioAtualizadoComoAdministrador()
        {
            _user = UserInfoFake.GetUserBruna();
            _user.Administrator = true;
        }

        [When(@"executar a url via PUT")]
        public async Task QuandoExecutarAUrlViaPUT()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_user));
            _httpResponseMessage = await _integrationTestFixture.Client.PutAsync($"/api/v1/user", content);
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(json))
                _result = Utils.Deserializer<GenericActionResult<UserInfoViewModel>>(json);
        }
        
        [Then(@"a api retornará status code (.*)")]
        public void EntaoAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }
        
        [Then(@"a mensagem '(.*)'")]
        public void EntaoAMensagem(string p0)
        {
            _result.Messages.Should().Contain(p0);
        }
    }
}
