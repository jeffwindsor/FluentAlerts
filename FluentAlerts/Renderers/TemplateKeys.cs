using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FluentAlerts.Renderers
{
    public enum TemplateKeys
    {
        //BODY
        SerializationHeader,
        SerializationFooter,

        //Sections 
        AlertHeader,
        AlertFooter,

        //TEXT - NORMAL => ROW + SPANNING VALUE
        TextNormalHeader,
        TextNormalFooter,

        //TEXT - TITLE
        TextTitleHeader,
        TextTitleFooter,

        //TEXT - EMPAHSIZED => ROW + EMPHASIZED VALUE
        TextEmphasizedHeader,
        TextEmphasizedFooter,

        //ROW 
        GroupHeader,
        GroupFooter,

        //VALUE NORMAL
        ValueNormalHeader,
        ValueNormalValue,
        ValueNormalFooter,

        //EMPHASIZED VALUE
        ValueEmphasizedHeader,
        ValueEmphasizedValue,
        ValueEmphasizedFooter,

        //SPANNING VALUE
        ValueSpanningHeader,
        ValueSpanningValue,
        ValueSpanningFooter,

        //URL 
        UrlHeader,
        UrlValue,
        UrlFooter,

        //SEPERATOR
        SeperatorHeader,
        SeperatorValue,
        SeperatorFooter,
    }

}
