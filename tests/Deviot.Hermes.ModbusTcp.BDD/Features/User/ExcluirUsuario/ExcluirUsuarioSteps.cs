using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.ExcluirUsuario
{
    [Binding]
    [Scope(Feature = "Excluir usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class ExcluirUsuarioSteps
    {
        [Given(@"que tenho um token de acesso admin")]
        public void DadoQueTenhoUmTokenDeAcessoAdmin()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um id de usuário válido")]
        public void DadoQueTenhoUmIdDeUsuarioValido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que que tenho um id de usuário inválido")]
        public void DadoQueQueTenhoUmIdDeUsuarioInvalido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário válido")]
        public void DadoQueTenhoUmUsuarioValido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um token de acesso normal")]
        public void DadoQueTenhoUmTokenDeAcessoNormal()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url via DELETE")]
        public void QuandoExecutarAUrlViaDELETE()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url via PUT")]
        public void QuandoExecutarAUrlViaPUT()
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
