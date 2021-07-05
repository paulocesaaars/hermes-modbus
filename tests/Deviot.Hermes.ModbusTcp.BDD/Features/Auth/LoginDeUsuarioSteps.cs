using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.Auth
{
    [Binding]
    [ExcludeFromCodeCoverage]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class LoginDeUsuarioSteps : IntegrationTestBase
    {
        private Login _login;
        private HttpResponseMessage _httpResponseMessage;

        public LoginDeUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity, 
                                   ITestOutputHelper testOutputHelper
                                  ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {

        }

        [Given(@"que tenho um username e senha válidos")]
        public void DadoQueTenhoUmUsernameESenhaValidos()
        {
            _login = new Login("admin", "admin");
        }
        
        [Given(@"que tenho um username ou senha invalido")]
        public void DadoQueTenhoUmUsernameOuSenhaInvalido()
        {
            _login = new Login("admin", "123456");
        }
        
        [When(@"executar a url de login via POST")]
        public async Task QuandoExecutarAUrlDeLogin()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_login));
            _httpResponseMessage = await _integrationTestFixture.Client.PostAsync($"/api/v1/auth/login", content);
        }
        
        [Then(@"A api retornará um stutus code (.*)")]
        public void EntaoAApiRetornaraUmStutusCode(int p0)
        {
            p0.Should().Equals(_httpResponseMessage.StatusCode);
        }
        
        [Then(@"um token de acesso valido")]
        public async Task EntaoUmTokenDeAcessoValido()
        {
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();
            var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(json);

            result.Data.Should().NotBeNull();
        }
        
        [Then(@"uma mensagem de erro: usuário ou senha inválidos")]
        public async Task EntaoUmaMensagemDeErroUsuarioOuSenhaInvalidos()
        {
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();
            var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(json);

            result.Data.Should().BeNull();
            result.Messages.Should().Contain("Usuário ou senha inválidos.");
        }

        [Then(@"uma mensagem de erro: '(.*)'")]
        public async Task EntaoUmaMensagemDeErro(string p0)
        {
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();
            var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(json);

            result.Data.Should().BeNull();
            result.Messages.Should().Contain(p0);
        }

    }
}
