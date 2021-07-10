using Deviot.Hermes.ModbusTcp.BDD.Fixtures;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.EditarUsuario
{
    [Binding]
    [Scope(Feature = "Editar usuário")]
    [Collection(nameof(IntegrationApiTestFixtureCollection))]
    public class EditarUsuarioSteps
    {
        [Given(@"que tenho um token de acesso admin")]
        public void DadoQueTenhoUmTokenDeAcessoAdmin()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário atualizado diferente do meu")]
        public void DadoQueTenhoUmUsuarioAtualizadoDiferenteDoMeu()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um token de acesso normal")]
        public void DadoQueTenhoUmTokenDeAcessoNormal()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho meu usuário atualizado")]
        public void DadoQueTenhoMeuUsuarioAtualizado()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que que tenho um usuário com id inválido")]
        public void DadoQueQueTenhoUmUsuarioComIdInvalido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com nome inválido")]
        public void DadoQueTenhoUmUsuarioComNomeInvalido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com senha inválida")]
        public void DadoQueTenhoUmUsuarioComSenhaInvalida()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário com nome já existente")]
        public void DadoQueTenhoUmUsuarioComNomeJaExistente()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um usuário válido")]
        public void DadoQueTenhoUmUsuarioValido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho meu usuário atualizado para administrador")]
        public void DadoQueTenhoMeuUsuarioAtualizadoParaAdministrador()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url via PUT")]
        public void QuandoExecutarAUrlViaPUT()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url PUT")]
        public void QuandoExecutarAUrlPUT()
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
