using System;
using FluentAlerts.Transformers;
using FluentAlerts.Renderers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts.Examples
{
    internal class ObjectFactory
    {
        #region Ioc
        //SIMULATE Inversion of Control or some other construction method
        // life time is single use 

        public static IAlertBuilderFactory CreateDefaultAlertBuilderFactory()
        {
            return new AlertBuilderFactory(CreateDefaultAlertFactory());
        }

        public static IAlertRenderer CreateDefaultAlertRender()
        {
            return new AlertRenderer(CreateDefaultAlertTransformer(), 
                CreateDefaultAlertTemplateRender());
        }
        

        private static IAlertFactory CreateDefaultAlertFactory()
        {
            return new AlertFactory<Alert>();
        }

        private static ITransformer<string> CreateDefaultAlertTransformer()
        {
            return new NameValueRowTransformer(new DefaultTransformStrategy(),
                                               new DefaultTypeInfoSelector(),
                                               new DefaultToStringFormatter(),
                                               CreateDefaultAlertBuilderFactory());
        }

        private static RenderTemplate CreateDefaultAlertTemplate()
        {
            var templates = new RenderTemplateDictionary(
                    new RenderTemplateDictionaryIssueHandler(CreateDefaultAlertBuilderFactory()));
            var settings = new FluentAlertDefaultedAppConfigSettings();
            return templates.GetTemplate(settings.DefaultTemplateName());
        }

        private static ITemplateRender CreateDefaultAlertTemplateRender()
        {
            return new DecorationBasedTemplateRender(CreateDefaultAlertTemplate());
        }
        #endregion

        #region Exceptions
        public static Exception GetNestedException(int nestingDepth)
        {
            try
            {
                ThrowNestedException(nestingDepth);
            }
            catch (Exception ex)
            {
                return ex;
            }

            //Should not happen, but compiler required it for all paths
            return null;
        }

        private static void ThrowNestedException(int nestingDepth)
        {
            if (nestingDepth == 0)
            {
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth));
            }
            try
            {
                ThrowNestedException(nestingDepth - 1);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Depth {0}", nestingDepth), ex);
            }
        }       

        #endregion
    }
}
