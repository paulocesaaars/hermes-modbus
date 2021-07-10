using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.Auth
{
    [Binding]
    [Scope(Feature = "Login de usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class LoginDeUsuarioSteps : IntegrationTestBase
    {
        private LoginViewModel _login;
        private GenericActionResult<TokenViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public LoginDeUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                   ITestOutputHelper testOutputHelper
                                  ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {

        }

        [Given(@"que tenho um username e senha válidos")]
        public void DadoQueTenhoUmUsernameESenhaValidos()
        {
            _login = GetAdminLogin();
        }
        
        [Given(@"que tenho um username ou senha invalido")]
        public void DadoQueTenhoUmUsernameOuSenhaInvalido()
        {
            _login = GetInvalidLogin();
        }
        
        [When(@"executar a url via POST")]
        public async Task QuandoExecutarAUrlViaPOST()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_login));
            _httpResponseMessage = await _integrationTestFixture.Client.PostAsync($"/api/v1/auth/login", content);

            var json = await _httpResponseMessage.Content.ReadAsStringAsync();
            _result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(json);
        }
        
        [Then(@"a api retornará um stutus code (.*)")]
        public void EntaoAApiRetornaraUmStutusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }
        
        [Then(@"um token de acesso valido")]
        public void EntaoUmTokenDeAcessoValido()
        {
            _result.Data.AccessToken.Should().NotBeNull();
        }
        
        [Then(@"uma mensagem de erro: '(.*)'")]
        public void EntaoUmaMensagemDeErro(string p0)
        {
            _result.Data.Should().BeNull();
            _result.Messages.Should().Contain(p0);
        }
    }
}
