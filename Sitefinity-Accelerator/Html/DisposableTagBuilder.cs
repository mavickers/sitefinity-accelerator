using System;
using System.Web.Mvc;

namespace SitefinityAccelerator.Html
{
    public sealed class DisposableTagBuilder : TagBuilder, IDisposable
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;

        public DisposableTagBuilder(string tagName, ViewContext viewContext, object htmlAttributes = null) : base(tagName)
        {
            this._viewContext = viewContext;

            if (htmlAttributes != null)
            {
                MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            }

            Begin();
        }

        private void Begin()
        {
            _viewContext.Writer.Write(ToString(TagRenderMode.StartTag));
        }

        private void End()
        {
            _viewContext.Writer.Write(ToString(TagRenderMode.EndTag));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                _disposed = true;

                End();
            }
        }
    }
}