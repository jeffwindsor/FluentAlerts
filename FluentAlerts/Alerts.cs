using System;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts
{
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
                                                   new DefaultFormatter());
            }

            public static ITransformer<string> CreateNameTypeValue()
            {
                return new NameTypeValueRowTransformer(new DefaultTransformStrategy(),
                                                   new DefaultTypeInfoSelector(),
                                                   new DefaultFormatter());
            }
        }
    }
}
