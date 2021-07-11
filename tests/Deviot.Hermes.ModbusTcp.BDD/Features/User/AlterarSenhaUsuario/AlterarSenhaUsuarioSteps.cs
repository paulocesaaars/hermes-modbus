using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fakes;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.AlterarSenhaUsuario
{
    [Binding]
    [Scope(Feature = "Alterar senha usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class AlterarSenhaUsuarioSteps : IntegrationTestBase
    {
        private UserPasswordViewModel _userPassword;
        private GenericActionResult<UserInfoViewModel> _result;
        private HttpResponseMessage _httpResponseMessage;

        public AlterarSenhaUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                  ITestOutputHelper testOutputHelper
                                 ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {
        }

        [Given(@"que tenho meu token de acesso")]
        public async Task DadoQueTenhoMeuTokenDeAcesso()
        {
            var token = await GetTokenAsync(GetPauloLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [Given(@"que tenho uma nova senha de usuário")]
        public void DadoQueTenhoUmaNovaSenhaDeUsuario()
        {
            _userPassword = UserPasswordFake.GetUserPaulo();
        }
        
        [Given(@"que tenho um token de acesso admin")]
        public async Task DadoQueTenhoUmTokenDeAcessoAdmin()
        {
            var token = await GetTokenAsync(GetAdminLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [Given(@"que tenho uma nova senha de usuário com a senha atual incorreta")]
        public void DadoQueTenhoUmaNovaSenhaDeUsuarioComASenhaAtualIncorreta()
        {
            _userPassword = UserPasswordFake.GetUserPaulo();
            _userPassword.Password = "654321";
        }
        
        [When(@"executar a url via PUT")]
        public async Task QuandoExecutarAUrlViaPUT()
        {
            var content = Utils.CreateStringContent(Utils.Serializer(_userPassword));
            _httpResponseMessage = await _integrationTestFixture.Client.PutAsync($"/api/v1/user/change-password", content);
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
