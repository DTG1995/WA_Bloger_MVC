using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace WA_Blogers_MVC.Controllers
{
    public class ImagesController : Controller
    {
        //
        // GET: /Images/
        public ActionResult Index(string path)
        {
            var pathP = path ?? "~/Content/images/thuvien";
            var physicalPath = Server.MapPath(pathP);
            var listImage = new List<string>();
            try
            {
                var absolutePaths = Directory.EnumerateFiles(physicalPath);
                listImage = absolutePaths.Select(x => pathP +"/"+ Path.GetFileName(x)).ToList();
            }
            catch {
                listImage = null;
            }
            ViewBag.Path = pathP;
            return View(listImage);
        }
        [HttpGet]
        public ActionResult UploadImages()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImages(HttpPostedFileBase filebase, string path)
        {
            foreach (string upload in Request.Files)
            {
                if (Request.Files[upload].ContentLength == 0) continue;
                string pathToSave = Server.MapPath(path);
                string filename = Path.GetFileName(Request.Files[upload].FileName);
                Request.Files[upload].SaveAs(Path.Combine(pathToSave, filename));
            }
            return View();
        }
        [HttpGet]
        public ActionResult UpLoadImage()
        {
            return View();
        }
        public ActionResult UpLoadImage(HttpPostedFileBase filebase, string path)
        {
            return View();
        }
	}
}