using Deviot.Common;
using Deviot.Common.Models;
using Deviot.Hermes.ModbusTcp.Api;
using Deviot.Hermes.ModbusTcp.Api.ViewModels;
using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using System;
using System.Net;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xunit.Abstractions;

namespace Deviot.Hermes.ModbusTcp.BDD.Bases
{
    public abstract class IntegrationTestBase
    {
        protected readonly IntegrationTestFixture<Startup> _integrationTestFixture;
        protected readonly ITestOutputHelper _testOutputHelper;

        private const string TOKEN_ERROR = "Houve um problema ao buscar o token";

        protected IntegrationTestBase(IntegrationTestFixture<Startup> integrationTestFixture,
            ITestOutputHelper testOutputHelper)
        {
            _integrationTestFixture = integrationTestFixture;
            _testOutputHelper = testOutputHelper;
        }

        protected LoginViewModel GetAdminLogin()
        {
            return new LoginViewModel { UserName = "admin", Password = "admin" };
        }

        protected LoginViewModel GetNormalLogin()
        {
            return new LoginViewModel { UserName = "paulo", Password = "123456" };
        }

        protected LoginViewModel GetInvalidLogin()
        {
            return new LoginViewModel { UserName = "admin", Password = "12345" };
        }

        protected async Task<string> GetTokenAsync(LoginViewModel login)
        {
            try
            {
                var content = Utils.CreateStringContent(Utils.Serializer(login));
                var request = await _integrationTestFixture.Client.PostAsync($"/api/v1/auth/login", content);

                if (request.StatusCode == HttpStatusCode.OK)
                {
                    var response = await request.Content.ReadAsStringAsync();
                    var result = Utils.Deserializer<GenericActionResult<TokenViewModel>>(response);
                        return result.Data.AccessToken;
                }
            }
            catch (Exception exception)
            {
                _testOutputHelper.WriteLine(exception.Message);
                throw new Exception(TOKEN_ERROR, exception);
            }

            return string.Empty;
        }
    }
}
