using System;
using FluentAlerts.Renderers;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts
{

    //UNDONE: Examples of steps and workflows, how tos 
    //UNDONE: Examples for how to use IAlerts, tranformed or not into razor
    //UNDONE: Alerts & IoC, plus maybe even allow configuration of defaults via config file
    //TODO: allow transformation without formatting? for above? nto sure that helps? 
    //TODO: split out interfaces into files
    //TODO: helper/extension methods methods for serialization of exception and alert?
    //TODO: add full comments and documentation (see using code comments with code examples)
    //TODO: ASK make (object o, IEnumerable<string> objectMemberPath) into RuleRequest, and maybe derived classes for format, strat and transform?
    //HACK: use of static Alerts class internally poses IOC and extendability issues...


    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    public static class Alerts
    {
        public static IAlertBuilder Create()
        {
            return CreateAlertBuilder();
        }

        public static IAlertBuilder Create(string title)
        {
           return Create().WithTitleOf(title);
        }
         
        public static IAlertBuilder Create(Exception ex)
        {
            return CreateAlertBuilder().WithValue(ex);
        }

        private static AlertBuilder CreateAlertBuilder()
        {
            return new AlertBuilder(new AlertFactory<Alert>(), new AlertItemCollection());
        }

        public static class Transformers
        {
            public static ITransformer<string> CreateDefault()
            {
                return new NameValueRowTransformer(new DefaultTransformStrategy(),
                                                   new DefaultTypeInfoSelector(),
                                                   new DefaultToStringFormatter());
            }

            public static ITransformer<string> CreateNameTypeValue()
            {
                return new NameTypeValueRowTransformer(new DefaultTransformStrategy(),
                                                   new DefaultTypeInfoSelector(),
                                                   new DefaultToStringFormatter());
            }
        }

        public static class Renders
        {
            public static IAlertRenderer CreateDefault()
            {
                return CreateDefault(new HtmlCssRenderTemplace());
            }

            public static IAlertRenderer CreateDefault(IRenderTemplate template)
            {
                return new AlertRenderer(Alerts.Transformers.CreateDefault(), template);
            }
        }
    }
}
