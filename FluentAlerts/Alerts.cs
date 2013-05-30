using System;
using FluentAlerts.Renderers;
using FluentAlerts.Settings;
using FluentAlerts.Transformers;
using FluentAlerts.Transformers.Formatters;
using FluentAlerts.Transformers.Strategies;
using FluentAlerts.Transformers.TypeInformers;

namespace FluentAlerts
{
    //** FUTURES **
    //UNDONE: Examples of steps and workflows, how tos. use request examples until something more generic is found
    //UNDONE: Examples for how to use IAlerts, tranformed or not into razor
    //UNDONE: Alerts & IoC, plus maybe even allow configuration of defaults via config file
    //UNDONE: Improve Fluent design so it can be expanded
    //UNDONE: Filesystem watcher for imported files to deal with change in file, so we can cache the loads
    //UNDONE : Using Trace for now
    //** NOWS **
    //TODO: rename url to uri
    //TODO: allow transformation without formatting? for above? nto sure that helps? 
    //TODO: split out interfaces into files
    //TODO: helper/extension methods methods for serialization of exception and alert?
    //TODO: add full comments and documentation (see using code comments with code examples)
    //TODO: ASK make (object o, IEnumerable<string> objectMemberPath) into RuleRequest, and maybe derived classes for format, strat and transform?
    //TODO: Extension namespace stratgey - so that others can add or remove at will
    //TODO: Text block to Group, with special accesor, blend text sytle and group style 
    //TODO: Rename Group to Array
    //TODO: PUBLIC CONSTANTS OR READONLY showing available active keys, and some docs on what is required
    //TODO: Validate
    //TODO: Validation routine so template files can be validated at any time., what to ddo about changes, allowing real time mods of files requires loda nad validate each time (slower) maybe check for file changes and load into static memeory? gets complicated but may but worth wile, check how NCalc does it for expressions, may just compare and revalidate

    //HACK: use of static Alerts class internally poses IOC and extendability issues...
    //HACK: Validate these: use spans or divs? 


    /// <summary>
    /// Static Factory for non IoC implementations
    /// </summary>
    public static class Factory
    {
        public static class Alerts
        {
            public static IAlertBuilder Create()
            {
                return CreateAlertBuilder();
            }

            public static IAlertBuilder Create(string title)
            {
                return Create().WithHeaderOne(title);
            }

            public static IAlertBuilder Create(Exception ex)
            {
                return CreateAlertBuilder().WithValue(ex);
            }
            
            private static AlertBuilder CreateAlertBuilder()
            {
                return new AlertBuilder(new AlertFactory<AlertList>(), new AlertItemCollection());
            }
        }

        public static class Transformers
        {
            public static ITransformer<string> Create()
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




        internal static class Issues
        {
            //UNDONE: Render Template File not found - make rule based or configable
            public static Template HandleRenderTemplateNotFound(string templateName)
            {
                var appSettings = new AppSettings();

                //Throw Exception
                Alerts.Create("Render Template Not Found")
                    .WithRow("Template Name", templateName)
                    .WithRow("Default Template Name", appSettings.DefaultTemplateName())
                    .WithRow("Templates File", appSettings.TemplateFileName())
                    .Throw();

                //???? empty for compiler happiness
                return null;
            }

            //UNDONE: Render Template File not valid - make rule based or configable
            //UNDONE: Cyclic references - make rule based or configable
            //UNDONE: Obtain value failure - make rule based or configable
            //UNDONE: No info for type - make rule based or configable
            //UNDONE: Null value - make rule based or configable

        }
    }
}
