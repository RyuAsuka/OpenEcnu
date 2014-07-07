using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OpenEcnu.AuthorizationServer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute("Default", "{controller}/{action}", new { controller = "Home", action = "Index" });

            routes.MapRoute("Oauth", "{controller}/{action}",
                    new
                    {
                        controller = "Oauth",
                        action = "Authorize",
                        client_id = UrlParameter.Optional,
                        redirect_uri = UrlParameter.Optional,
                        response_type = UrlParameter.Optional,
                        state = UrlParameter.Optional
                    });
        }
    }
}
