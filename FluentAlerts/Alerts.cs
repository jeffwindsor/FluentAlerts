using System;
using FluentAlerts.Renderers;
using FluentAlerts.Settings;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts
{
   

    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    //public static class Factory
    //{
   //    public static class Transformers
    //    {
    //        public static ITransformer<string> Create() 
    //        {
    //            return new NameValueRowTransformer(new DefaultTransformStrategy(),
    //                                               new DefaultTypeInfoSelector(),
    //                                               new DefaultToStringFormatter());
    //        }

    //        public static ITransformer<string> CreateNameTypeValue()
    //        {
    //            return new NameTypeValueRowTransformer(new DefaultTransformStrategy(),
    //                                               new DefaultTypeInfoSelector(),
    //                                               new DefaultToStringFormatter());
    //        }
    //    }


    //    internal static class Issues
    //    {
    //        //UNDONE: Render Template File not found - make rule based or configable
    //        public static Template HandleRenderTemplateNotFound(string templateName)
    //        {
    //            var appSettings = new AppSettings();

    //            //Throw Exception
    //            Alerts.Create("Render Template Not Found")
    //                .WithRow("Template Name", templateName)
    //                .WithRow("Default Template Name", appSettings.DefaultTemplateName())
    //                .WithRow("Templates File", appSettings.TemplateFileName())
    //                .Throw();

    //            //???? empty for compiler happiness
    //            return null;
    //        }



    //    }
    //}
}
