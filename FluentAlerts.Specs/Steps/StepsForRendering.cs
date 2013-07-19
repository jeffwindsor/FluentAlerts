using System.IO;
using System.Linq;
using FluentAlerts.Renderers;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace FluentAlerts.Specs
{
    [Binding]
    public class RenderSteps
    {
        //private const string DefaultTemplateName = "HtmlWithEmbeddedCssTableTemplate";
        //private const string TestTemplateName = "TestTemplate";
        //private const char TestMemberPathSeperator = ':';
        
        private IFluentAlertSettings _appsettings;
        private RenderTemplateDictionary _templateDictionary;
        //private AlertRenderTemplateDictionary _otherTemplateDictionary;
        private RenderTemplate _alertRenderTemplate;
        //private AlertRenderTemplate _otherAlertRenderTemplate;
        private ITemplateRender _alertTemplateRender;
        private IAlertRenderer _render;
        private string _renderResult;

        private readonly AlertContext _context;
        public RenderSteps(AlertContext context)
        {
            _context = context;
        }

        [Given(@"I have custom app settings")]
        private void GivenIHaveCustomAppSettings()
        {
            _context.Settings = new TestFluentAlertSettings();
            _appsettings = _context.Settings;
        }

        [Given(@"I set the default template name to (.*)")]
        public void GivenISetTheDefaultTemplateNameTo(string name)
        {
            _context.Settings.DefaultTemplateName = name;
        }

        //[Given(@"I set the default template file location to (.*)")]
        //public void GivenISetTheDefaultTemplateFileLocationTo(string fileName)
        //{
        //    _context.Settings.TemplateFileName = fileName;
        //}

        //[Given(@"I have a template file at (.*)")]
        //public void GivenIHaveATemplateFileAt(string fileName)
        //{
        //    File.Copy(TestTemplateFilePath, fileName, true);
        //}
        
        //[Given(@"I have the default app settings")]
        //public void GivenIHaveADefaultAppSettings()
        //{
        //    _appsettings = new FluentAlertDefaultedAppConfigSettings();
        //}

        //[Given(@"I have a template issue handler")]
        //public void GivenIHaveATemplateIssueHandler()
        //{
        //    
        //}

        [Given(@"I import the render templates")]
        public void GivenIImportTheRenderTemplates()
        {
            var issueHandler = new RenderTemplateDictionaryIssueHandler(_context.AlertBuilderFactory);
            _templateDictionary = new RenderTemplateDictionary(issueHandler);
            _templateDictionary.Import();
        } 

        [Given(@"I have a (.*) template")]
        public void GivenIHaveANamedTemplate(string templateName)
        {
            _alertRenderTemplate = _templateDictionary.GetTemplate(templateName); 
        }

        [Given(@"I have a template render")]
        public void GivenIHaveATemplateRender()
        {
            _alertTemplateRender = new TemplateRenderer(_alertRenderTemplate);
        }

        [Given(@"I have an alert render")]
        public void GivenIHaveAnAlertRender()
        {
            _render = new AlertRenderer(_context.Transformer, _alertTemplateRender);
        }


        //[When(@"I import the templates")]
        //public void WhenIImportTheTemplates()
        //{
        //    ScenarioContext.Current.Pending();
        //}

        //[When(@"delete all local template files")]
        //public void WhenDeleteAllLocalTemplateFiles()
        //{
        //    ScenarioContext.Current.Pending();
        //}

        //[When(@"I export the templates")]
        //public void WhenIExportTheTemplates()
        //{
        //    ScenarioContext.Current.Pending();
        //}
         
        //[Then(@"the template dictionaries contains all the local templates")]
        //public void ThenTheTemplateDictionariesContainsAllTheLocalTemplates()
        //{
        //    ScenarioContext.Current.Pending();
        //}


        [When(@"I render the alert")]
        public void WhenIRenderTheAlert()
        {
            _renderResult = _render.Render(_context.Alert);
        }

        //[When(@"I create a new template dictionary from (.*)")]
        //public void WhenICreateANewTemplateDictionaryFrom(string fileName)
        //{
        //    if (fileName == "Default")
        //        fileName = DefaultTemplateFilePath;

        //    _otherTemplateDictionary = new AlertRenderTemplateDictionary(_alertRenderTemplateIssueHandler);
        //    _otherTemplateDictionary.Import();
        //}

        //[When(@"I export the templates to (.*)")]
        //public void WhenIExportTheTemplatesTo(string fileName)
        //{
        //    _templateDictionary.Export(fileName);
        //}

        [When(@"I get the default template")]
        public void WhenIHaveADefaultTemplate()
        {
            _alertRenderTemplate = _templateDictionary.GetTemplate(_appsettings.DefaultTemplateName());
        }

        //[When(@"I get the (.*) as the other template")]
        //public void WhenIGetTheOtherNamedTemplate(string templateName)
        //{
        //    _otherAlertRenderTemplate = _templateDictionary.GetTemplate(templateName);
        //}


        [Then(@"the rendered text has the default formatting")]
        public void ThenTheRenderedTextHasTheDefaultFormatting()
        {
            File.WriteAllText("render_output.html", _renderResult);
            _renderResult.Length.Should().NotBe(0);
        }
  
        //[Then(@"the templates are equivilant")]
        //public void ThenTheTemplatesAreEquivilant()
        //{
        //    _alertRenderTemplate.ShouldBeEquivalentTo(_otherAlertRenderTemplate);
        //}
    
        //[Then(@"the template dictionaries are equivilant")]
        //public void ThenTheTemplateDictionariesAreEquivilant()
        //{
        //    _templateDictionary.Keys.ShouldBeEquivalentTo(_otherTemplateDictionary.Keys);
        //    foreach (var key in _templateDictionary.Keys) 
        //    {
        //        _templateDictionary.GetTemplate(key).ShouldBeEquivalentTo(_otherTemplateDictionary.GetTemplate(key));
        //    }
        //}

        //[Then(@"a backup of the original (.*) is written to the same directory")]
        //public void ThenABackupOfTheOriginalIsWrittenToTheSameDirectory(string fileName)
        //{
        //    var searchPattern =GetSearchPattern(fileName);
        //    var f = from fi in Directory.GetFiles(Directory.GetCurrentDirectory() ,searchPattern)
        //            orderby fi descending
        //            select fi;

        //    var o = File.ReadAllText(TestTemplateFilePath); 
        //    var n = File.ReadAllText(f.First());
        //    o.Should().Be(n);
        //}

        //[Then(@"clean up file (.*)")]
        //public void ThenCleanUpFile(string fileName)
        //{
        //    var searchPattern = GetSearchPattern(fileName);
        //    foreach (var fn in Directory.GetFiles(Directory.GetCurrentDirectory(), searchPattern))
        //    {
        //        File.Delete(fn);
        //    }
        //}

        //[Then(@"the settings default template name is the Html with Embedded Css template")]
        //public void ThenTheSettingsDefaultTemplateNameIsTheDefault()
        //{
        //    _appsettings.DefaultTemplateName().Should().Be(DefaultTemplateName);
        //}


        private static string GetSearchPattern(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);
            return string.Format("{0}*{1}",
                                 Path.GetFileNameWithoutExtension(fileName),
                                 Path.GetExtension(fileName));
        }

    }
}
