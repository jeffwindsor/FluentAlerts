using System;
using FluentAlerts.Serializers;

namespace FluentAlerts.Extensions
{
    /// <summary>
    /// Extensions for IAlerts
    /// </summary>
    /// <remarks>
    /// This is a partial static class for extension 
    /// (best effort at open-closed principle with a static class)
    /// </remarks>
    public static class IAlertExtensions
    {
        //TODO Serialization: need to allow the extension / replacement / configuration of serializers. maybe via IoC?

        //HACK: could not think of a better name, and of course ToString is used...

        /// <summary>
        /// Will serialize the Alert to Text 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ToText(this IFluentAlert source, Func<string,string> modifyResults = null)
        {
            var s = new AlertSerializerToText();
            return s.Serialize(source, modifyResults);
        }

        public static string ToHtml(this IFluentAlert source, Func<string, string> modifyResults = null)
        {
            var s = new AlertSerializerToHtml();
            return s.Serialize(source, modifyResults);
        } 
    }
}
