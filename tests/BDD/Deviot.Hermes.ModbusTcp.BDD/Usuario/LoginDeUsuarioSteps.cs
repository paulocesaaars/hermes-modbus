using System;
using TechTalk.SpecFlow;

namespace Deviot.Hermes.ModbusTcp.BDD.Usuario
{
    [Binding]
    public class LoginDeUsuarioSteps
    {
        [Given(@"que tenho um username e senha válidos")]
        public void DadoQueTenhoUmUsernameESenhaValidos()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"que tenho um username ou senha invalido")]
        public void DadoQueTenhoUmUsernameOuSenhaInvalido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"executar a url de login")]
        public void QuandoExecutarAUrlDeLogin()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"passar as informações via POST")]
        public void QuandoPassarAsInformacoesViaPOST()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"A api retornará um stutus code (.*)")]
        public void EntaoAApiRetornaraUmStutusCode(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"um token de acesso valido")]
        public void EntaoUmTokenDeAcessoValido()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"uma mensagem de erro: usuário ou senha inválidos")]
        public void EntaoUmaMensagemDeErroUsuarioOuSenhaInvalidos()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
