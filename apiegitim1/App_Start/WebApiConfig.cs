using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace apiegitim1
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.EnableCors();
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
               routeTemplate: "api/{controller}/{action}/{id}", // action metot ismi demek
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
