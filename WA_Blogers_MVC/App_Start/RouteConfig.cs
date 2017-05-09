using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WA_Blogers_MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //my change
            routes.MapRoute(
                name: "ViewPostsInBlogs",
                url: "{controller}/{title}-{id}",
                defaults: new { controller = "Blogs", action = "ViewBlogs", id = UrlParameter.Optional, title = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "Login",
                url: "Login",
                defaults: new {controller = "Authentication", action = "Login"}
                );
            routes.MapRoute(
                name: "Logout",
                url: "Logout",
                defaults: new { controller = "Authentication", action = "Logout" }
                );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
