using Deviot.Hermes.ModbusTcp.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.BDD.Fixtures
{
    [CollectionDefinition(nameof(IntegrationApiTestFixtureCollection))]
    public class IntegrationApiTestFixtureCollection : ICollectionFixture<IntegrationTestFixture<Startup>>
    {
    }

    public class IntegrationTestFixture<TStartup> : IDisposable where TStartup : class
    {
        public TestServer Server { get; private set; }

        public HttpClient Client { get; private set; }

        public IntegrationTestFixture()
        {
            var clientOptions = new WebApplicationFactoryClientOptions()
            {
                HandleCookies = false,
                BaseAddress = new Uri("http://localhost"),
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 7
            };

            var builder = new WebHostBuilder().UseStartup<Startup>()
                                              .UseEnvironment("Development");

            Server = new TestServer(builder)
            {
                BaseAddress = new Uri("http://localhost"),
            };

            Client = Server.CreateClient();
        }

        public void AddToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new InvalidOperationException("O token não foi informado");

            Client.DefaultRequestHeaders.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void Dispose()
        {
            Client.Dispose();
            Server.Dispose();
        }
    }
}
