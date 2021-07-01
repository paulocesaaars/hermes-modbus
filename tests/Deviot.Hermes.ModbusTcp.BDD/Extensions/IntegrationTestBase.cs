using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using Deviot.Hermes.ModbusTcp.BDD.Helpers;
using Deviot.Hermes.ModbusTcp.Business.Entities;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Extensions
{
    public abstract class IntegrationTestBase
    {
        protected readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        protected readonly ITestOutputHelper _testOutputHelper;

        protected IntegrationTestBase(IntegrationTestFixture<Startup> integrationTestFixture,
            ITestOutputHelper testOutputHelper)
        {
            _integrationTestFixture = integrationTestFixture;
            _testOutputHelper = testOutputHelper;
        }

        protected async Task<string> GetTokenAsync(string userName, string password)
        {
            try
            {
                var content = JsonHelper.CreateStringContent(JsonHelper.Serializer(new { UserName = userName, Password = password }));
                var request = await _integrationTestFixture.Client.PostAsync($"/api/v1/auth/login", content);

                if (request.StatusCode == HttpStatusCode.OK)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = JsonHelper.Deserializer<TestActionResult<Token>>(response);
                        return result.Data.AccessToken;
                }
            }
            catch (Exception exception)
            {
                _testOutputHelper.WriteLine(exception.Message);
                throw new Exception("Houve um problema ao buscar o token", exception);
            }

            return string.Empty;
        }
    }
}
