using System.Net.Http;
using Clever.Service;
using System.Web.Http;
using System;
using System.Net;

namespace Clever.WebApi
{
    public interface IResourceController<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        HttpResponseMessage Get(TId id);
        HttpResponseMessage Get(TQuery query);
        HttpResponseMessage Post(TPost data);
        HttpResponseMessage Put(TId id, TPut data);
        HttpResponseMessage Delete(TId id);
        TSession GetSession();
    }

    public abstract class ResourceControler<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
        : ApiController, IResourceController<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession>
    {
        protected readonly IResourceService<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession> resourceService;
        public ResourceControler(IResourceService<TId, TGetById, TGetMany, TQuery, TPost, TPostReturn, TPut, TPutReturn, TDeleteReturn, TSession> resourceService)
        {
            this.resourceService = resourceService;
        }

        public virtual HttpResponseMessage Delete(TId id)
        {
            return HandleErrors(() => resourceService.Delete(id, GetSession()));
        }

        public virtual HttpResponseMessage Get(TQuery query)
        {
            return HandleErrors(() => resourceService.Get(query, GetSession()));
        }

        public virtual HttpResponseMessage Get(TId id)
        {
            return HandleErrors(() => resourceService.Get(id, GetSession()));
        }

        public virtual HttpResponseMessage Post(TPost data)
        {
            return HandleErrors(() => resourceService.Post(data, GetSession()));
        }

        public virtual HttpResponseMessage Put(TId id, TPut data)
        {
            return HandleErrors(() => resourceService.Put(id, data, GetSession()));
        }
        
        [NonAction]
        public abstract TSession GetSession();

        protected HttpResponseMessage HandleErrors<T>(Func<ServiceOption<T>> something)
        {
            try
            {
                return something().ToHttpResponseMessage(Request);
            }
            catch(NotImplementedException e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, String.Format("Method {0} not implemented or not supported for resource {1}", Request.Method, Request.RequestUri.AbsolutePath));
            }
        }
    }
}
