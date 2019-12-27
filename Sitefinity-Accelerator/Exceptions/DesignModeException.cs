using System;
using System.Web;
using SitefinityAccelerator.Extensions;

namespace SitefinityAccelerator.Exceptions
{
    public class DesignModeException : SystemException
    {
        private HtmlString _htmlMessage;

        public HtmlString HtmlMessage => string.IsNullOrWhiteSpace(_htmlMessage.ToString()) ? Message.ToHtmlString() : _htmlMessage;

        public DesignModeException(string message = "This widget is disabled in design mode") : base(message)
        {
            _htmlMessage = new HtmlString(message);
        }
    }
}