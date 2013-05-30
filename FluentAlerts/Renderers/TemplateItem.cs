
namespace FluentAlerts.Renderers
{
    public enum TemplateItem
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
        ValueNormalFooter,

        //EMPHASIZED VALUE
        ValueEmphasizedHeader,
        ValueEmphasizedFooter,

        //SPANNING VALUE
        ValueSpanningHeader,
        ValueSpanningFooter,

        //SPANNING EMPHAISZED VALUE
        ValueEmphasizedSpanningHeader,
        ValueEmphasizedSpanningFooter,

        //URL 
        UrlHeader,
        UrlFooter,

        //SEPERATOR
        SeperatorHeader,
        SeperatorFooter,
    }

}
