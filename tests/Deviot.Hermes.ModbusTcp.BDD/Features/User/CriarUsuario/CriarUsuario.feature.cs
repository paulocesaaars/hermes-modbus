﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Deviot.Hermes.ModbusTcp.BDD.Features.User.CriarUsuario
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CriarUsuarioFeature : object, Xunit.IClassFixture<CriarUsuarioFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private string[] _featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "CriarUsuario.feature"
#line hidden
        
        public CriarUsuarioFeature(CriarUsuarioFeature.FixtureData fixtureData, Deviot_Hermes_ModbusTcp_BDD_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "Features/User/CriarUsuario", "Criar usuário", "\tComo consumidor de api quero criar um usuário", ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public virtual void TestInitialize()
        {
        }
        
        public virtual void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public virtual void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Criar usuário com sucesso")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Criar usuário com sucesso")]
        public virtual void CriarUsuarioComSucesso()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Criar usuário com sucesso", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 4
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 5
testRunner.Given("que tenho um token de acesso admin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 6
testRunner.And("que tenho um usuário válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 7
testRunner.When("executar a url via POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 8
testRunner.Then("a api retornará status code 201", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 9
testRunner.And("a mensagem \'O usuário foi criado com sucesso\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nome de usuário inválido")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Nome de usuário inválido")]
        public virtual void NomeDeUsuarioInvalido()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nome de usuário inválido", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 11
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 12
testRunner.Given("que tenho um token de acesso admin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 13
testRunner.And("que tenho um usuário com nome inválido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 14
testRunner.When("executar a url via POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 15
testRunner.Then("a api retornará status code 403", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 16
testRunner.And("a mensagem \'O nome de usuário precisar ter somente valores alfanuméricos ou under" +
                        "line\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Senha de usuário inválida")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Senha de usuário inválida")]
        public virtual void SenhaDeUsuarioInvalida()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Senha de usuário inválida", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 18
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 19
testRunner.Given("que tenho um token de acesso admin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 20
testRunner.And("que tenho um usuário com senha inválida", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 21
testRunner.When("executar a url via POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 22
testRunner.Then("a api retornará status code 403", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 23
testRunner.And("a mensagem \'A senha precisar ter somente valores alfanuméricos\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Nome de usuário já existente")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Nome de usuário já existente")]
        public virtual void NomeDeUsuarioJaExistente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Nome de usuário já existente", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 25
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 26
testRunner.Given("que tenho um token de acesso admin", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 27
testRunner.And("que tenho um usuário com nome já existente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 28
testRunner.When("executar a url POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 29
testRunner.Then("a api retornará status code 403", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 30
testRunner.And("a mensagem \'O nome de usuário informado já existe\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Erro de autenticação")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Erro de autenticação")]
        public virtual void ErroDeAutenticacao()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Erro de autenticação", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 32
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 33
testRunner.Given("que tenho um usuário válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 34
testRunner.When("executar a url via POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 35
testRunner.Then("a api retornará status code 401", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Erro de autorização")]
        [Xunit.TraitAttribute("FeatureTitle", "Criar usuário")]
        [Xunit.TraitAttribute("Description", "Erro de autorização")]
        public virtual void ErroDeAutorizacao()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Erro de autorização", null, tagsOfScenario, argumentsOfScenario, this._featureTags);
#line 37
this.ScenarioInitialize(scenarioInfo);
#line hidden
            bool isScenarioIgnored = default(bool);
            bool isFeatureIgnored = default(bool);
            if ((tagsOfScenario != null))
            {
                isScenarioIgnored = tagsOfScenario.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((this._featureTags != null))
            {
                isFeatureIgnored = this._featureTags.Where(__entry => __entry != null).Where(__entry => String.Equals(__entry, "ignore", StringComparison.CurrentCultureIgnoreCase)).Any();
            }
            if ((isScenarioIgnored || isFeatureIgnored))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 38
testRunner.Given("que tenho um token de acesso normal", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 39
testRunner.And("que tenho um usuário válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 40
testRunner.When("executar a url via POST", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 41
testRunner.Then("a api retornará status code 401", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
#line 42
testRunner.And("a mensagem \'Somente um administrador pode criar ou deletar um usuário\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                CriarUsuarioFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                CriarUsuarioFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
