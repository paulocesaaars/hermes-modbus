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
        private UserInfoViewModel _user;
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
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com nome inválido")]
        public void DadoQueTenhoUmUsuarioComNomeInvalido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com senha inválida")]
        public void DadoQueTenhoUmUsuarioComSenhaInvalida()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com nome já existente")]
        public void DadoQueTenhoUmUsuarioComNomeJaExistente()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um token de acesso normal")]
        public void DadoQueTenhoUmTokenDeAcessoNormal()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url via POST")]
        public void QuandoExecutarAUrlViaPOST()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url POST")]
        public void QuandoExecutarAUrlPOST()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a api retornará status code (.*)")]
        public void EntaoAApiRetornaraStatusCode(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"a mensagem '(.*)'")]
        public void EntaoAMensagem(string p0)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
