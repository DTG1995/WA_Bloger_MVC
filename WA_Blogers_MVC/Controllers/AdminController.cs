using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using WA_Blogers_MVC.Models;
using System.IO;
using System.Data.Entity;
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
        public JsonResult ChangeTopNav(List<string> listNameTopNav, List<string> listUrlTopNav, List<string> txtUrlAd, List<string> listSocialName, List<string> listSocialUrl)
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
        [HttpPost]
        public ActionResult ChangeMidleTop(HttpPostedFileBase fileBase, string txtUrlAd, string useClock)
        {
            var time = db.WA_Options.Where(x => x.Name.Equals("timeclock")).FirstOrDefault();
            string imgAds="";
            string imgLogo="";
            string path = "~/Public/";
            foreach (string upload in Request.Files)
            {
                
                if (Request.Files[upload].ContentLength == 0) continue;
                string pathToSave = Server.MapPath(path);
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(pathToSave, filename));
                if (upload.Equals("logoPage"))
                    imgLogo = filename;
                else imgAds = filename;
            }
            if(!String.IsNullOrEmpty(imgLogo))
            {
                var logo = db.WA_Options.Where(x => x.Name.Equals("logo")).FirstOrDefault();
                if(logo != null)
                {
                    logo.Value = imgLogo;
                    db.Entry(logo).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.WA_Options.Add(new WA_Options { Name = "logo", Value = imgLogo });
                    db.SaveChanges();
                }
            }
            if(useClock.Equals("true"))
            {
                
                if(time==null)
                {
                    db.WA_Options.Add(new WA_Options { Name = "timeclock", Value = "<div class=\"clock\" style=\"margin:0px;width:460px;float:right;max-height: 90px;\" ></div>" });
                    db.SaveChanges();
                }
            }
            else
            {
                db.WA_Options.Remove(time);
                db.SaveChanges();
                var ads = db.WA_Options.Where(x => x.Group.Equals("Ads")).ToList();
                if (ads != null)
                {
                    if(ads.Count<2)
                    {
                        db.WA_Options.RemoveRange(ads);
                        ads.Add(new WA_Options { Name = "AdsLink", Value = txtUrlAd, Group = "Ads" });
                        ads.Add(new WA_Options { Name = "AdsImage", Value = imgAds, Group = "Ads" });
                        db.WA_Options.AddRange(ads);
                        db.SaveChanges();
                    }
                    else
                    {
                        for (int i = 0; i < 2; i++)
                        {
                            if (ads[i].Name == "AdsLink")
                                ads[i].Value = txtUrlAd;
                            else ads[i].Value = imgAds;
                        }
                    }
                }
                else
                {
                    ads.Add(new WA_Options { Name = "AdsLink", Value = txtUrlAd, Group = "Ads" });
                    ads.Add(new WA_Options { Name = "AdsImage", Value = imgAds, Group = "Ads" });
                    db.WA_Options.AddRange(ads);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }
	}
}