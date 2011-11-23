using System;
using System.Collections.Generic;
using System.Text;

namespace Alerts
{
    //HACK: Formats are hard coded, could inject a "format" or schema.  Left this for later since not sure which way it will go. 
    internal class AlertSerializerToHtml : AlertSerializer
    {
        private const string HEADER_BACKGROUND_COLOR = "silver";
        private const string HIGHLIGHT_BACKGROUND_COLOR = "silver";
        private const string NORMAL_COLUMN_HEADER_BACKGROUND_COLOR = "gainsboro";
        private const string NORMAL_COLUMN_BACKGROUND_COLOR = "whitesmoke";
        private const string FONT_FAMILY = "font-family:Arial,Sans-Serif";

        protected override void AppendSerialization(AlertSeperator source, StringBuilder sb)
        {
            sb.Append("<HR>");
        }

        protected override void AppendSerialization(AlertTextBlock source, StringBuilder sb)
        {
            dynamic text = source.Text.ToString().Replace(Environment.NewLine, "<BR>");
            switch (source.Style)
            {
                case TextStyle.Header1:
                    sb.AppendFormat("<span style=\"{1};font-size:11pt;font-weight:bold;\">{0}</span><BR>", text, FONT_FAMILY);

                    break;
                case TextStyle.Normal:
                    sb.AppendFormat("<span style=\"{1};font-size:9pt;\">{0}</span><BR>", text, FONT_FAMILY);

                    break;
                case TextStyle.Bold:
                    sb.AppendFormat("<span style=\"{1};font-size:9pt;font-weight:bold;\">{0}</span><BR>", text, FONT_FAMILY);

                    break;
            }
        }

        protected override void AppendSerialization(AlertURL source, StringBuilder sb)
        {
            sb.AppendFormat("<span style=\"{2};font-size:9pt;font-weight:bold;\"><a href=\"{0}\">{1}</a></span><BR>", source.Url, source.Text, FONT_FAMILY);
        }

        protected override void AppendSerialization(AlertTable source, StringBuilder sb)
        {
            sb.AppendFormat("<table cellspacing='1' cellpadding='2' style='{0};font-size:8pt;' width='100%'>", FONT_FAMILY);
            foreach (var row in source.Rows)
            {
                AppendSerialization(row, source.ColumnCount, sb);
            }
            sb.Append("</table>");
        }

        protected override void AppendSerialization(AlertTable.Row source, int columns, StringBuilder sb)
        {
            switch (source.Style)
            {
                case RowStyle.Normal:
                    //Normal Row
                    AppendSerialization(source.Values, columns, NORMAL_COLUMN_HEADER_BACKGROUND_COLOR, NORMAL_COLUMN_BACKGROUND_COLOR, sb);

                    break;
                case RowStyle.Header:
                    //Column Spanning Header
                    sb.AppendFormat("<TR><TD bgcolor='{0}' colspan='{1}'><B>{2}</B></TD></TR>", HEADER_BACKGROUND_COLOR, columns, source.Values[0]);

                    break;
                case RowStyle.Highlight:
                    //Highlighted Row
                    AppendSerialization(source.Values, columns, HIGHLIGHT_BACKGROUND_COLOR, HIGHLIGHT_BACKGROUND_COLOR, sb);

                    break;
                case RowStyle.Footer:
                    //Column Spanning Footer
                    sb.AppendFormat("<TR><TD bgcolor='{0}' colspan='{1}'><B>{2}</B></TD></TR>", HEADER_BACKGROUND_COLOR, columns, source.Values[0]);

                    break;
            }
        }

        private void AppendSerialization(IList<object> values, int columns, string firstColumnBackground, string otherColumnsBackground, StringBuilder sb)
        {
            sb.Append("<TR>");
            for (var col = 0; col <= (columns - 1); col++)
            {
                sb.AppendFormat("<TD bgcolor='{0}'>", col == 0 && values.Count > 1 ? firstColumnBackground : otherColumnsBackground);
                if (col < values.Count)
                {
                    var value = values[col];

                    //Check For embedded Alert Tables and Messages
                    if (value is CompositeAlert)
                    {
                        AppendSerialization((CompositeAlert)value, sb);
                    }
                    else if (value is AlertTable)
                    {
                        AppendSerialization((AlertTable)value, sb);
                    }
                    else
                    {
                        sb.Append(value);
                    }

                }
                sb.Append("</TD>");
            }
            sb.Append("</TR>");
        }

    }
 
}
