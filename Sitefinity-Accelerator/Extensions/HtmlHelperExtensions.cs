using System;
using System.Web;
using System.Web.Mvc;
using HtmlAgilityPack;
using SitefinityAccelerator.Html;

namespace SitefinityAccelerator.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString ConditionalTagAttribute(this HtmlHelper htmlHelper, string attribute, string value)
        {
            if (string.IsNullOrWhiteSpace(attribute) || string.IsNullOrWhiteSpace(value))
            {
                return MvcHtmlString.Empty;
            }

            var trimmedAttribute = attribute.Trim();
            var trimmedValue = value.Trim();

            return new MvcHtmlString($@"{trimmedAttribute}=""{trimmedValue}""");
        }

        public static MvcHtmlString ConditionalJsonProperty(this HtmlHelper htmlHelper, string property, string value, bool withComma = true)
        {
            if (string.IsNullOrWhiteSpace(property) || string.IsNullOrWhiteSpace(value))
            {
                return MvcHtmlString.Empty;
            }

            var trimmedProperty = property.Trim();
            var trimmedValue = value.Trim();

            return new MvcHtmlString($@"{trimmedProperty}: ""{trimmedValue}""{(withComma ? "," : "")}");
        }

        public static IDisposable ConditionalTagWrapper(this HtmlHelper htmlHelper, string tagName, bool condition = true, object htmlAttributes = null)
        {
            return condition ? new DisposableTagBuilder(tagName, htmlHelper.ViewContext, htmlAttributes) : null;
        }

        public static string ToPlainString(this IHtmlString htmlString)
        {
            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(htmlString.ToHtmlString());

            return HtmlEntity.DeEntitize(document.DocumentNode.InnerText);
        }

    }
}