using Clever.Service;
using System;
using System.Web;

namespace Clever.Web
{
    public abstract class HttpContextService<T> : IContextService<T>
    {
        protected readonly HttpContext context;
        private Func<HttpContext, T> getSessionData;

        protected HttpContextService(HttpContext httpContext)
        {
            this.context = httpContext;
        }

        protected HttpContextService(HttpContext httpContext, Func<HttpContext, T> getSessionData): this(httpContext)
        {
            this.getSessionData = getSessionData;
        }

        public virtual T SessionData { get { return getSessionData(context); } }
    }
}
