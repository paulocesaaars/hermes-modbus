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

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.BuscarUsuario
{
    [Binding]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class BuscarUsuarioSteps : IntegrationTestBase
    {
        private string _id;
        private string _jsonResult;
        private HttpResponseMessage _httpResponseMessage;

        public BuscarUsuarioSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                   ITestOutputHelper testOutputHelper
                                  ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {

        }

        [Given(@"que tenho um token de acesso")]
        public async Task DadoQueTenhoUmTokenDeAcesso()
        {
            var token = await GetTokenAsync(GetAdminLogin());
            _integrationTestFixture.AddToken(token);
        }
        
        [Given(@"que tenho um id de usuário válido")]
        public void DadoQueTenhoUmIdDeUsuarioValido()
        {
            _id = UserInfoFake.GetUserAdmin().Id.ToString();
        }
        
        [Given(@"que que tenho um id de usuário inválido")]
        public void DadoQueQueTenhoUmIdDeUsuarioInvalido()
        {
            _id = Guid.NewGuid().ToString();
        }
        
        [When(@"executar a url via GET")]
        public async Task QuandoExecutarAUrlViaGET()
        {
            _httpResponseMessage = await _integrationTestFixture.Client.GetAsync($"/api/v1/user/{_id}");
            _jsonResult = await _httpResponseMessage.Content.ReadAsStringAsync();
        }
        
        [Then(@"a api retornará status code (.*)")]
        public void EntaoAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Equals(_httpResponseMessage.StatusCode);
        }
        
        [Then(@"o usuário desejado")]
        public void EntaoOUsuarioDesejado()
        {
            var result = Utils.Deserializer<GenericActionResult<UserInfoViewModel>>(_jsonResult);

            var user = UserInfoFake.GetUserAdmin();

            result.Data.Should().BeEquivalentTo(user);
        }
        
        [Then(@"a mensagem '(.*)'")]
        public void EntaoAMensagem(string p0)
        {
            var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(_jsonResult);

            result.Data.Should().BeNull();
            result.Messages.Should().Contain(p0);
        }
    }
}
