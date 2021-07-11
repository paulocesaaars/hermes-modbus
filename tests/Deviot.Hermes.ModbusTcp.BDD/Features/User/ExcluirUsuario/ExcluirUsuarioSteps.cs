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

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.ExcluirUsuario
{
    [Binding]
    [Scope(Feature = "Excluir usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ExcluirUsuarioSteps : IntegrationTestBase
    {
        private string _id;
        private GenericActionResult<UserInfoViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public ExcluirUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
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
        
        [Given(@"que tenho um id de usuário válido")]
        public void DadoQueTenhoUmIdDeUsuarioValido()
        {
            _id = UserInfoFake.GetUserPaula().Id.ToString();
        }
        
        [Given(@"que que tenho um id de usuário inválido")]
        public void DadoQueQueTenhoUmIdDeUsuarioInvalido()
        {
            _id = Guid.NewGuid().ToString();
        }

        [Given(@"que tenho um id de administrador válido")]
        public void DadoQueTenhoUmIdDeAdministradorValido()
        {
            _id = UserInfoFake.GetUserAdmin().Id.ToString();
        }


        [Given(@"que tenho um token de acesso normal")]
        public async Task DadoQueTenhoUmTokenDeAcessoNormal()
        {
            var token = await GetTokenAsync(GetPauloLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [When(@"executar a url via DELETE")]
        public async Task QuandoExecutarAUrlViaDELETE()
        {
            _httpResponseMessage = await _integrationTestFixture.Client.DeleteAsync($"/api/v1/user/{_id}");
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
