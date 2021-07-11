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

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.CriarUsuario
{
    [Binding]
    [Scope(Feature = "Criar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class CriarUsuarioSteps : IntegrationTestBase
    {
        private UserViewModel _user;
        private GenericActionResult<UserInfoViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public CriarUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
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
        
        [Given(@"que tenho um usuário válido")]
        public void DadoQueTenhoUmUsuarioValido()
        {
            _user = new UserViewModel
            {
                FullName = "Novo usuário",
                UserName = "novo_usuario",
                Password = "123456",
                Administrator = false,
                Enabled = true
            };
        }
        
        [Given(@"que tenho um usuário com nome inválido")]
        public void DadoQueTenhoUmUsuarioComNomeInvalido()
        {
            _user = new UserViewModel
            {
                FullName = "Novo usuário",
                UserName = "novo usuario",
                Password = "123456",
                Administrator = false,
                Enabled = true
            };
        }
        
        [Given(@"que tenho um usuário com senha inválida")]
        public void DadoQueTenhoUmUsuarioComSenhaInvalida()
        {
            _user = new UserViewModel
            {
                FullName = "Novo usuário",
                UserName = "novo_usuario",
                Password = "123@456",
                Administrator = false,
                Enabled = true
            };
        }
        
        [Given(@"que tenho um usuário com nome já existente")]
        public void DadoQueTenhoUmUsuarioComNomeJaExistente()
        {
            _user = new UserViewModel
            {
                FullName = "Novo usuário",
                UserName = "paulo",
                Password = "123456",
                Administrator = false,
                Enabled = true
            };
        }
        
        [Given(@"que tenho um token de acesso normal")]
        public async Task DadoQueTenhoUmTokenDeAcessoNormal()
        {
            var token = await GetTokenAsync(GetPauloLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [When(@"executar a url via POST")]
        public async Task QuandoExecutarAUrlViaPOST()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_user));
            _httpResponseMessage = await _integrationTestFixture.Client.PostAsync($"/api/v1/user", content);
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
