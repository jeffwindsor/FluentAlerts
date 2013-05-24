using System;
using System.Collections.Generic;

namespace FluentAlerts.Renderers
{
    public interface ITemplateRender
    {
        string GetSerializationHeader();
        string GetSerializationFooter();

        string GetAlertHeader();
        string GetAlertFooter();
        
        string GetTextBlockHeader(TextStyle style);
        string GetTextBlockFooter(TextStyle style);

        string GetGroupHeader(GroupStyle style);
        string GetGroupFooter(GroupStyle style);

        string GetValueHeader(GroupStyle style, int index, int indexMax);
        string GetValueFooter(GroupStyle style, int index, int indexMax);
    }

    public class TemplateRenderer : ITemplateRender
    {
        private Template _template;

        public TemplateRenderer(Template template)
        {
            _template = template;
        }

        public string GetSerializationHeader()
        {
            throw new NotImplementedException();
        }

        public string GetSerializationFooter()
        {
            throw new NotImplementedException();
        }

        public string GetAlertHeader()
        {
            throw new NotImplementedException();
        }

        public string GetAlertFooter()
        {
            throw new NotImplementedException();
        }

        public string GetTextBlockHeader(TextStyle style)
        {
            throw new NotImplementedException();
        }

        public string GetTextBlockFooter(TextStyle style)
        {
            throw new NotImplementedException();
        }

        public string GetGroupHeader(GroupStyle style)
        {
            throw new NotImplementedException();
        }

        public string GetGroupFooter(GroupStyle style)
        {
            throw new NotImplementedException();
        }

        public string GetValueHeader(GroupStyle style, int index, int indexMax)
        {
            throw new NotImplementedException();
        }

        public string GetValueFooter(GroupStyle style, int index, int indexMax)
        {
            throw new NotImplementedException();
        }
    }
}
