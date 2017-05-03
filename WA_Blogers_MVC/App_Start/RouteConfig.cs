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
                url: "{controller}/{title}",
                defaults: new { controller = "Blogs", action = "ViewBlogs", id = UrlParameter.Optional, title = "" }
                );






            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            
        }
    }
}
