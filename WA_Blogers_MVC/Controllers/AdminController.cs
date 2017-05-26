using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;
namespace WA_Blogers_MVC.Controllers
{
    public class AdminController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult ChangeTopNav(List<string> listNameTopNav, List<string> listUrlTopNav){
            var list = db.WA_Options.Where(x=>x.Group.Equals("Topnav") || x.Group.Equals("TopnavSocical"));
            db.WA_Options.RemoveRange(list);
            db.SaveChanges();
            
            return Json(list);

        }
	}
}