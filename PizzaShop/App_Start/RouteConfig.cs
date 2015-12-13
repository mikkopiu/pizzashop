using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PizzaShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                constraints: new { controller = GetAllControllersAsRegex() }
            );

            // Catch-all route
            routes.MapRoute(
                name:  "NotFound",
                url: "{*url}",
                defaults: new { controller = "Error", action = "PageNotFound" }
            );
        }

        private static string GetAllControllersAsRegex() {
            var controllers = typeof(MvcApplication).Assembly
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(Controller)));
            var controllerNames = controllers.Select(c => c.Name.Replace("Controller", ""));

            return string.Format("({0})", string.Join("|", controllerNames));
        }
    }
}
