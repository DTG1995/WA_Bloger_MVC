using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;

namespace WA_Blogers_MVC.Controllers
{
    public class HomeController : Controller
    {
        private  WA_BlogerEntities db = new WA_BlogerEntities();
        public ActionResult Index()
        {
            ViewBag.ListTopNews = db.WA_Posts.OrderBy(x => x.Created).Take(5).ToList();
            ViewBag.ListPostFeatured = db.WA_Posts.OrderBy(x => x.Seen).Take(8).ToList();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}