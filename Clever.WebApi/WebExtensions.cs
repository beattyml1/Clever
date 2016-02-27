using Clever.Service;
using System;
using System.Net;
using System.Net.Http;

namespace Clever.WebApi
{
    public static class WebExtensions
    {
        public static HttpResponseMessage ToHttpResponseMessage<T>(this ServiceOption<T> result, HttpRequestMessage request)
        {
            return result.IsDelayed ? HandleAsync(result, request) :
                   result.HasError ? request.CreateErrorResponse(GetHttpStatusCode(result.Error), result.ErrorMessage) :
                   result.HasValue ? request.CreateResponse(result.Value) :
                   null;
        }

        private static HttpResponseMessage HandleAsync<T>(ServiceOption<T> result, HttpRequestMessage request)
        {
            return request.CreateResponse(result.Promise);
        }

        private static HttpStatusCode GetHttpStatusCode(ServiceError error)
        {
            return HttpStatusCode.BadRequest;
        }
    }
}
