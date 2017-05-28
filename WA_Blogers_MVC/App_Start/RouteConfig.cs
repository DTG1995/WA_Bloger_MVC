﻿using System;
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
                url: "chu-de/{title}-{id}",
                defaults: new { controller = "Blogs", action = "ViewBlogs", id = UrlParameter.Optional, title = UrlParameter.Optional }
                );
            routes.MapRoute(
                name: "ViewPost",
                url: "bai-viet/{title}-{id}",
                defaults: new { controller = "Posts", action = "ViewPost", id = UrlParameter.Optional, title = UrlParameter.Optional }
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
                name: "EditPost",
                url: "admin/chinh-sua-bai-viet/{title}-{id}",
                defaults: new { controller = "Posts", action = "Edit", id = UrlParameter.Optional, title=UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "ChiTietPost",
                url: "admin/chi-tiet-bai-viet/{title}-{id}",
                defaults: new { controller = "Posts", action = "Details", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "XoaPost",
                url: "admin/xoa-bai-viet-{id}",
                defaults: new { controller = "Posts", action = "Delete", id = UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "QuanLyBlog",
                url: "Admin/quan-ly-blog",
                defaults: new { controller = "Blogs", action = "Index" }
            );

            routes.MapRoute(
               name: "XoaBlog",
               url: "Admin/xoa-blog/{id}",
               defaults: new { controller = "Blogs", action = "Delete", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "ChiTietBlog",
              url: "Admin/chi-tiet-blog/{title}-{id}",
              defaults: new { controller = "Blogs", action = "Details", id = UrlParameter.Optional, title = UrlParameter.Optional }
          );
            routes.MapRoute(
                name: "SuaNguoiDung",
                url: "nguoi-dung/sua-nguoi-dung-{id}",
                defaults: new { controller = "Users", action = "Edit", id = UrlParameter.Optional }
                );
            
            routes.MapRoute(
                name: "TrangChuNguoiDung",
                url: "nguoi-dung",
                defaults: new { controller = "Users", action = "Index" }
                );
            routes.MapRoute(
              name: "ChinhSuaBlog",
              url: "Admin/chinh-sua-blog/{title}-{id}",
              defaults: new { controller = "Blogs", action = "Edit", id = UrlParameter.Optional, title = UrlParameter.Optional }
          );
            routes.MapRoute(
              name: "ThemBlogMoi",
              url: "Admin/them-blog-moi",
              defaults: new { controller = "Blogs", action = "Create"}
          );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
