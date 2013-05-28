using System;
using TechTalk.SpecFlow;

namespace FluentAlerts.Specs
{
    [Binding]
    public class RenderSteps
    {
        private AlertContext _context;
        public RenderSteps(AlertContext context)
        {
            _context = context;
        }
        
        [Given(@"I have a Custom template render")]
        public void GivenIHaveACustomTemplateRender()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I set the templates location in the config")]
        public void GivenISetTheTemplatesLocationInTheConfig()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I have a file at that location")]
        public void GivenIHaveAFileAtThatLocation()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Given(@"I set the default template in the config")]
        public void GivenISetTheDefaultTemplateInTheConfig()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I render the alert")]
        public void WhenIRenderTheAlert()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I export the templates to a file")]
        public void WhenIExportTheTemplatesToAFile()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I create a Template Dictionary")]
        public void WhenICreateATemplateDictionary()
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"I create a default render")]
        public void WhenICreateADefaultRender()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the rendered text has the Custom formatting")]
        public void ThenTheRenderedTextHasTheCustomFormatting()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the deafult templates are in file")]
        public void ThenTheDeafultTemplatesAreInFile()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the template dictionary contains TestTemplate(.*) template")]
        public void ThenTheTemplateDictionaryContainsTestTemplateTemplate(int p0)
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the template dictionary contains HtmlWithEmbeddedStylesTableTemplate template")]
        public void ThenTheTemplateDictionaryContainsHtmlWithEmbeddedStylesTableTemplateTemplate()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the render uses the template in the config")]
        public void ThenTheRenderUsesTheTemplateInTheConfig()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the rendered text has the plain HTML formatting")]
        public void ThenTheRenderedTextHasThePlainHTMLFormatting()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
