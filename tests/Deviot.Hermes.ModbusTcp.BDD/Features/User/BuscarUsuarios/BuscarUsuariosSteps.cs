using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fakes;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.BuscarUsuarios
{
    [Binding]
    [Scope(Feature = "Buscar usuários")]
    public class BuscarUsuariosSteps : IntegrationTestBase
    {
        private GenericActionResult<IEnumerable<UserInfoViewModel>> _result;
        private HttpResponseMessage _httpResponseMessage;

        public BuscarUsuariosSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
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
        
        [When(@"executar a url via GET")]
        public async Task QuandoExecutarAUrlViaGET()
        {
            _httpResponseMessage = await _integrationTestFixture.Client.GetAsync($"/api/v1/user");
            var json = await _httpResponseMessage.Content.ReadAsStringAsync();

            if (!string.IsNullOrEmpty(json))
                _result = Utils.Deserializer<GenericActionResult<IEnumerable<UserInfoViewModel>>>(json);
        }
        
        [Then(@"a api retornará status code (.*)")]
        public void EntaoAApiRetornaraStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }
        
        [Then(@"todos usuários do sistema")]
        public void EntaoTodosUsuariosDoSistema()
        {
            _result.Data.Count().Should().BeGreaterOrEqualTo(3);
            _result.Data.Should().ContainEquivalentOf(UserInfoFake.GetUserAdmin());
        }
    }
}
