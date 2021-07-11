using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.BDD.Bases;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using FluentAssertions;
using System.Net.Http;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.HeathCheck
{
    [Binding]
    [Scope(Feature = "HealthCheck")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class HealthCheckSteps : IntegrationTestBase
    {
        private HttpResponseMessage _httpResponseMessage;

        public HealthCheckSteps(IntegrationTestFixture<Startup> integrationTestFixtureIdentity,
                                ITestOutputHelper testOutputHelper
                               ) : base(integrationTestFixtureIdentity, testOutputHelper)
        {

        }

        [When(@"Executar a url via GET")]
        public async Task QuandoExecutarAUrlDeHeathchekViaGETAsync()
        {
            _httpResponseMessage = await _integrationTestFixture.Client.GetAsync($"/api/v1/health-check");
        }
        
        [Then(@"a api retornará um status code (.*)")]
        public void EntaoAApiRetornaraUmStatusCode(int p0)
        {
            p0.Should().Be((int)_httpResponseMessage.StatusCode);
        }
    }
}
