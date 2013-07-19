using System;
using System.Collections.Generic;
using System.Linq;
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

        #region ClassesAndStructs
        public static NestedTestClass CreateNestedTestClass(int nestingDepth)
        {
            return new NestedTestClass()
            {
                TestDate = DateTime.Now,
                TestNumber = (NumberEnum)nestingDepth,
                Date = DateTime.Now,
                Number = (NumberEnum)nestingDepth,
                Child = (nestingDepth < 1)
                            ? null
                            : CreateNestedTestClass(nestingDepth - 1),
                Children = (nestingDepth < 1)
                               ? Enumerable.Empty<NestedTestClass>()
                               : from i in Enumerable.Range(0, 5) select CreateNestedTestClass(nestingDepth - 1)
            };
        }

        public static NestedTestStruct GetNestedTestStruct(int nestingDepth)
        {
            return new NestedTestStruct()
            {
                TestDate = DateTime.Now,
                Number = (NumberEnum)nestingDepth,
                Children = (nestingDepth < 1)
                               ? Enumerable.Empty<NestedTestStruct>()
                               : from i in Enumerable.Range(0, 5) select GetNestedTestStruct(nestingDepth - 1)
            };
        }

        internal enum NumberEnum
        {
            One,
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven
        }

        internal class NestedTestClass
        {
            public DateTime TestDate { get; set; }
            public NumberEnum TestNumber;
            public DateTime Date;
            public NumberEnum Number;
            public NestedTestClass Child { get; set; }
            public IEnumerable<NestedTestClass> Children { get; set; }
        }

        internal struct NestedTestStruct
        {
            public DateTime TestDate { get; set; }
            public NumberEnum Number;
            public IEnumerable<NestedTestStruct> Children { get; set; }
        }
        #endregion
    }
}
