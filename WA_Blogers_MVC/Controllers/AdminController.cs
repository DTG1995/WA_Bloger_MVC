using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
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
        public JsonResult ChangeTopNav(List<string> listNameTopNav, List<string> listUrlTopNav, List<string> listSocialName, List<string> listSocialUrl)
        {
            var list = db.WA_Options.Where(x=>x.Group.Equals("Topnav") || x.Group.Equals("TopnavSocical"));
            db.WA_Options.RemoveRange(list);
            db.SaveChanges();
            var  listOptionNav =new List<WA_Options>();
            if (listNameTopNav != null)
            {
                for (int i = 0; i < listNameTopNav.Count; i++)
                {
                    string link = listUrlTopNav[i] ?? "#";
                    if (link.Length > 4 && !link.Substring(0, 4).Equals("http") && link != "#")
                        link = "http://" + link;
                    listOptionNav.Add(new WA_Options
                    {
                        Group = "Topnav",
                        Name = listNameTopNav[i],
                        Value = link
                    });
                }
            }
            if (listSocialName != null)
            {
                for (int i = 0; i < listSocialName.Count; i++)
                {
                    string link = listSocialUrl[i] ?? "#";
                    if (link.Length > 4 && !link.Substring(0, 4).Equals("http") && link != "#")
                        link = "http://" + link;
                    listOptionNav.Add(new WA_Options
                    {
                        Group = "TopnavSocical",
                        Name = listSocialName[i],
                        Value = link
                    });
                }
            }
            
            db.WA_Options.AddRange(listOptionNav);
            db.SaveChanges();
            return Json(listOptionNav, JsonRequestBehavior.AllowGet);

        }
	}
}