using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Exercise3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("display", "display/{ip}/{port}",
               defaults: new { controller = "First", action = "display", id = UrlParameter.Optional }
               );


            routes.MapRoute("displayFlight", "display/{fileName}/{time}",
              defaults: new { controller = "First", action = "displayFlight", id = UrlParameter.Optional }
              );


            routes.MapRoute("displaySave", "save/{ip}/{port}/{frequancy}/{time}/{fileName}",
                defaults: new { controller = "First", action = "displaySave", id = UrlParameter.Optional }
                );

            routes.MapRoute("displayByTime", "display/{ip}/{port}/{time}",
                defaults: new { controller = "First", action = "displayByTime", id = UrlParameter.Optional }
                );

           
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "First", action = "Index", id = UrlParameter.Optional }
            );

          
        }
    }
}
