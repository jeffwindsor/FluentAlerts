using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.Examples
{
    internal class Mother
    {
        //public static IFluentAlertSerializer CreateDefaultAlertRender()
        //{
        //    return new FluentAlertSerializer(
        //        CreateDefaultAlertTransformer(),
        //        new FluentAlertTemplateBasedTransformableSerializer(CreateDefaultAlertTemplate()));
        //}
        //private static SerializationTemplate CreateDefaultAlertTemplate()
        //{
        //    var templates = new RenderTemplateDictionary(new RenderTemplateDictionaryIssueHandler(new FluentAlerts()));
        //    var settings = new FluentAlertDefaultedAppConfigSettings();

        //    return templates.GetTemplate(settings.DefaultTemplateName());
        //}       

        //private static ITransformer CreateDefaultAlertTransformer()
        //{
        //    return new NameValueRowTransformer(new DefaultTransformStrategy(),
        //                                       new DefaultTypeInformerSelector(),
        //                                       new DefaultValueToStringFormatter(),
        //                                       new FluentAlerts());
        //}

        #region Exceptions
        public static Exception CreateNestedException(int nestingDepth)
        {
            try
            {
                ThrowNestedException(0,nestingDepth);
            }
            catch (Exception ex)
            {
                return ex;
            }

            //Should not happen, but compiler required it for all paths
            return null;
        }

        private static void ThrowNestedException(int nestingDepth, int maxDepth)
        {
            //At top, throw without a catch
            if (nestingDepth == maxDepth)
            {
                throw new ApplicationException(string.Format("Exception Message at Depth {0}", nestingDepth));
            }

            //Inside so re-curse
            try
            {
                ThrowNestedException(nestingDepth + 1, maxDepth);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Exception Message at Depth {0}", nestingDepth), ex);
            }
        }       

        #endregion

        #region ClassesAndStructs
        public static NestedTestClass CreateNestedTestObject(int nestingDepth)
        {
            return new NestedTestClass()
            {
                DateProperty = DateTime.Now,
                NumberProperty = (NumberEnum)nestingDepth,
                DateField = DateTime.Now,
                NumberField = (NumberEnum)nestingDepth,
                Child = (nestingDepth < 1)
                            ? null
                            : CreateNestedTestObject(nestingDepth - 1),
                Children = (nestingDepth < 1)
                               ? Enumerable.Empty<NestedTestClass>()
                               : from i in Enumerable.Range(0, 5) select CreateNestedTestObject(nestingDepth - 1)
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
            public DateTime DateProperty { get; set; }
            public NumberEnum NumberProperty;
            public DateTime DateField;
            public NumberEnum NumberField;
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
