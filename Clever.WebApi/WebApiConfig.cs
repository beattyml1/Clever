using Clever.WebApi;
using System.Web.Http;

namespace Clever.WebApi
{
    public static class WebApiConfig
    {
        public static void ConfigureBaseRoutes(IdType idType, HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            switch (idType)
            {
                case IdType.Int:
                    ConfigureBaseRoutes(config, "[0-9]+");
                    break;
                case IdType.GuidOrEncrypted:
                    ConfigureBaseRoutes(config, "-.*");
                    break;
                case IdType.Mixed:
                    ConfigureBaseRoutes(config, "([0-9]+)|(-.*)");
                    break;
            }
        }

        private static void ConfigureBaseRoutes(HttpConfiguration config, string idRegex)
        {
            config.Routes.MapHttpRoute(
                name: "Resource",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { },
                constraints: new
                {
                    id = idRegex
                }
            );

            config.Routes.MapHttpRoute(
                name: "ResourceActions",
                routeTemplate: "api/{controller}/{id}/{action}",
                defaults: new { },
                constraints: new
                {
                    id = idRegex,
                    action = "[a-zA-Z][a-zA-Z0-9]*"
                }
            );

            config.Routes.MapHttpRoute(
                name: "ResourceActions",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { },
                constraints: new
                {
                    action = "[a-zA-Z][a-zA-Z0-9]*"
                }
            );
        }
    }

    public enum IdType
    {
        Int, GuidOrEncrypted, Mixed
    }
}