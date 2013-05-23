using System;

namespace FluentAlerts.Renderers
{
    public class HtmlCssRenderTemplace: IRenderTemplate
    {
        public string GetSerializationHeader()
        {
            throw new NotImplementedException();
        }

        public string GetAlertHeader()
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

        public string GetValueHeader(GroupStyle style, int index, int indexMax)
        {
            throw new NotImplementedException();
        }

        public string GetValueFooter(GroupStyle style, int index, int indexMax)
        {
            throw new NotImplementedException();
        }

        public string GetGroupFooter(GroupStyle style)
        {
            throw new NotImplementedException();
        }

        public string GetAlertFooter()
        {
            throw new NotImplementedException();
        }

        public string GetSerializationFooter()
        {
            throw new NotImplementedException();
        }
    }
}
