using System.Text.RegularExpressions;

namespace TechShed.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        static public string HtmlToPlainText(this string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return html;
            html = Regex.Replace(html, @"(<[^>]*>|\r\n|\r|\n)", "");
            html = Regex.Replace(html, @"\s+", " ");

            return html;
        }


        static public string ToEllipse(this string text, int length = 20)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            if (length >= text.Length)
            {
                return text;
            }



            return $"{text.Substring(0, length)}...";
        }
    }
}
