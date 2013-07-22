using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentAlerts.TypeInformers
{
    public class DefaultTypeInfoSelector : BaseTypeInfoSelector 
    {
        public DefaultTypeInfoSelector()
        {
            Rules.Add( (info, obj, path) =>
                {
                    var type = obj.GetType();

                    //TYPE NAME
                    info.TypeName = type.Name;

                    //PROPERTIES AND FIELDS
                    if (type.IsSubclassOf(typeof (Exception)))
                    {
                        info.TypeName += " TEST TEST TEST";
                        //Limit exception shown properties
                        //TODO: more general ability move this to a type dictionary of property and field names loaded from file??? will have issues with sub classing??
                        var names = new List<string>(new[] {"Message", "StackTrace", "InnerException", "Alert"});
                        info.PropertyInfos = from name in names 
                                             join  pi in type.GetProperties() on name equals pi.Name
                                             select pi;
                        info.ClearFields();
                    }
                    else
                    {
                        //****************************************
                        //Default Type Info Selection Rules
                        //****************************************
                        //Properties: All public readable 
                        info.PropertyInfos = from pi in type.GetProperties()
                                             where pi.CanRead
                                             orderby pi.Name 
                                             select pi;
                        
                        //Fields: All public readable 
                        info.FieldInfos = from fi in type.GetFields()
                                          orderby fi.Name
                                          select fi;
                    }
                });
        }
    }
}
