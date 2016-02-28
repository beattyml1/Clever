using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Clever.WebApi
{
    public static class JsonConfig
    {
        public static void Configure()
        {
            var jsonFormatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
        }
    }
}
