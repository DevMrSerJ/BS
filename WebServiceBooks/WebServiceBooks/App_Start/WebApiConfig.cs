using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebServiceBooks
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API routes
            config.MapHttpAttributeRoutes();

            // json format answer
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
