using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WA_Blogers_MVC.Models;

namespace WA_Blogers_MVC.Controllers
{
    public class HomeController : Controller
    {
        private  WA_BlogerEntities db = new WA_BlogerEntities();
        public ActionResult Index(string q, int? page)
        {
            ViewBag.ListTopNews = db.WA_Posts.OrderBy(x => x.Created).Take(5).ToList();
            if (String.IsNullOrEmpty(q))
            {
                
                ViewBag.ListPostFeatured = db.WA_Posts.OrderBy(x => x.Seen).Take(8).ToList();

                return View();
            }
            else
            {
                ViewBag.StringSearch = q;
                int pageCurent = page ?? 1;
                var model = db.WA_Posts.Where(
                    x =>
                        x.Title.Contains(q))
                    .OrderByDescending(x => x.Created)
                    .ToPagedList(pageCurent, 6);
                return View("IndexSearch",model);
            }
        }

        
    }
}