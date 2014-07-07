using System.Web.Mvc;
using System.Web.Routing;

namespace OpenEcnu.UserInterface
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "AppOpers",
                url: "App/{action}/{appKey}",
                defaults: new {controller = "App", action = "Index", appKey = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "AppUsers",
                url: "App/Index/{userId}",
                defaults: new { controller = "App", action = "Index", userId = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //    name: "AppIndex",
            //    url: "App/Index/{userId}"
            //);

            //routes.MapRoute(
            //    name: "AppCreate",
            //    url: "App/Create"
            //);

            //routes.MapRoute(
            //    name: "AppDetail",
            //    url: "App/Detail/{appKey}"
            //);

            //routes.MapRoute(
            //    name: "AppDelete",
            //    url: "App/Delete/{appKey}"
            //);

            //routes.MapRoute(
            //    name: "AppModify",
            //    url: "App/Modify/{appKey}"
            //);

        }
    }
}
