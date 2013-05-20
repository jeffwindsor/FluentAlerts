﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:1.9.0.77
//      SpecFlow Generator Version:1.9.0.0
//      Runtime Version:4.0.30319.17929
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace FluentAlerts.Specs.TypeTransformations
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "1.9.0.77")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("Developer Can Specify Transformations Options")]
    [NUnit.Framework.CategoryAttribute("Serialization")]
    public partial class DeveloperCanSpecifyTransformationsOptionsFeature
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "DeveloperCanSpecifyTransformationOptions.feature"
#line hidden
        
        [NUnit.Framework.TestFixtureSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "Developer Can Specify Transformations Options", "In order to specify the information that is serialized\r\nAs a developer\r\nI want to" +
                    " be able to select the properties, methods and fields that will be serialized by" +
                    " type and at depth", ProgrammingLanguage.CSharp, new string[] {
                        "Serialization"});
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.TestFixtureTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Default transformer limits depth to zero for all non exception classes")]
        public virtual void DefaultTransformerLimitsDepthToZeroForAllNonExceptionClasses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Default transformer limits depth to zero for all non exception classes", ((string[])(null)));
#line 10
this.ScenarioSetup(scenarioInfo);
#line 11
 testRunner.Given("I have a object graph of depth 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 12
   testRunner.And("I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
  testRunner.When("I tranform the object", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 14
  testRunner.Then("the transformer does not create any sub alerts", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Default transformer does not limit depth for all exception classes")]
        public virtual void DefaultTransformerDoesNotLimitDepthForAllExceptionClasses()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Default transformer does not limit depth for all exception classes", ((string[])(null)));
#line 16
this.ScenarioSetup(scenarioInfo);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can specify transformation depth of an object to base properties")]
        [NUnit.Framework.CategoryAttribute("UnitTest")]
        public virtual void CanSpecifyTransformationDepthOfAnObjectToBaseProperties()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can specify transformation depth of an object to base properties", new string[] {
                        "UnitTest"});
#line 19
this.ScenarioSetup(scenarioInfo);
#line 20
 testRunner.Given("I have a object graph of depth 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 21
   testRunner.And("I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can specify transformation depth of a type")]
        public virtual void CanSpecifyTransformationDepthOfAType()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can specify transformation depth of a type", ((string[])(null)));
#line 23
this.ScenarioSetup(scenarioInfo);
#line 24
 testRunner.Given("I have a object graph of depth 5", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 25
      testRunner.And("I have a HeirarchialNameValueRowTransformer with a DefaultTypeInformer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can specify transformation depth of a type be unlimited")]
        public virtual void CanSpecifyTransformationDepthOfATypeBeUnlimited()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can specify transformation depth of a type be unlimited", ((string[])(null)));
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
 testRunner.Given("I have a object graph of <n> depth", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Defaults to selecting all properties and fields but no methods")]
        public virtual void DefaultsToSelectingAllPropertiesAndFieldsButNoMethods()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Defaults to selecting all properties and fields but no methods", ((string[])(null)));
#line 32
this.ScenarioSetup(scenarioInfo);
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can limit type selections")]
        [NUnit.Framework.CategoryAttribute("Extensibility")]
        public virtual void CanLimitTypeSelections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can limit type selections", new string[] {
                        "Extensibility"});
#line 36
this.ScenarioSetup(scenarioInfo);
#line 37
 testRunner.Given("inconclusive", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can add to type selections")]
        [NUnit.Framework.CategoryAttribute("Extensibility")]
        public virtual void CanAddToTypeSelections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can add to type selections", new string[] {
                        "Extensibility"});
#line 40
this.ScenarioSetup(scenarioInfo);
#line 41
 testRunner.Given("inconclusive", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can limit global selections")]
        [NUnit.Framework.CategoryAttribute("Extensibility")]
        public virtual void CanLimitGlobalSelections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can limit global selections", new string[] {
                        "Extensibility"});
#line 44
this.ScenarioSetup(scenarioInfo);
#line 45
 testRunner.Given("inconclusive", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Can add to global selections")]
        [NUnit.Framework.CategoryAttribute("Extensibility")]
        public virtual void CanAddToGlobalSelections()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Can add to global selections", new string[] {
                        "Extensibility"});
#line 48
this.ScenarioSetup(scenarioInfo);
#line 49
 testRunner.Given("inconclusive", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion