using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Script.Serialization;

namespace SitefinityAccelerator.Extensions
{
    public static class StringExtensions
    {
        public static string DefaultIfNullOrWhiteSpace(this string source, string defaultValue)
        {
            return string.IsNullOrWhiteSpace(source) ? defaultValue : source;
        }

        public static HtmlString ToHtmlString(this string source)
        {
            if (source == null)
            {
                return null;
            }

            return new HtmlString(source);
        }

        public static string JoinString(this IEnumerable<string> source, char joinChar)
        {
            if (source == null)
            {
                return null;
            }

            return string.Join(joinChar.ToString(), source);
        }

        public static string Serialize(this List<string> source)
        {
            if (source == null) throw new ArgumentNullException();

            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(source);
        }
    }
}