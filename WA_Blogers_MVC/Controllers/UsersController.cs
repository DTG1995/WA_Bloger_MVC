using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;
using PagedList;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WA_Blogers_MVC.Filter;

namespace WA_Blogers_MVC.Controllers
{
    public class UsersController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Users/
        [AdminFilter]
        public ActionResult Index(string q, int? numDisplay, string sort, int? page)
        {
            
            var user = from u in db.WA_Users
                        select u;
            ViewBag.CurrentSort=sort;
            
            //search
            if(!String.IsNullOrEmpty(q))
            {
                user = user.Where(s => s.UserName.Contains(q) || s.Email.Contains(q));
            }
            ViewBag.SearchName = q;
            ViewBag.SumUsers = user.Count();
            //sort
            ViewBag.NameSort = String.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.EmailSort = sort == "Email" ? "email_desc" : "Email";
            ViewBag.DisplaySort = sort == "DisplayName" ? "displayName_desc" : "DisplayName";
            switch(sort)
            {
                case"name_desc":
                    user = user.OrderByDescending(s => s.UserName);
                    break;
                case"Email":
                    user = user.OrderBy(s => s.Email);
                    break;
                case "email_desc":
                    user = user.OrderByDescending(s => s.Email);
                    break;
                case "DisplayName":
                    user = user.OrderBy(s => s.DisplayName);
                    break;
                case"displayName_desc":
                    user=user.OrderByDescending(s=>s.DisplayName);
                    break;
                default:
                    user=user.OrderBy(s=>s.UserName);
                    break;
            }
            //page
            int pageSize = 5;
            pageSize = numDisplay == 0 ? user.Count() :( numDisplay??5);
            ViewBag.NumDisplay = numDisplay;
            int pageNumber = (page ?? 1);

            return View(user.ToPagedList(pageNumber,pageSize));
        }

        // GET: /Users/Details/5
        [AdminFilter]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Users wa_users = db.WA_Users.Find(int.Parse(id));
            if (wa_users == null)
            {
                return HttpNotFound();
            }
            return View(wa_users);
        }

        // GET: /Users/Create
        [AdminFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Create([Bind(Include = "UserID,UserName,Email,Password,DisplayName,Avatar")] WA_Users wa_users, HttpPostedFileBase filebase)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0 || !String.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    string path = "~/Content/images/avatar";
                    string pathToSave = Server.MapPath(path);
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    Request.Files[0].SaveAs(Path.Combine(pathToSave, filename));
                    wa_users.Avatar = filename;
                }
                wa_users.Created = DateTime.Now;
                db.WA_Users.Add(wa_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wa_users);
        }

        // GET: /Users/Edit/5
        [AdminFilter]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Users wa_users = db.WA_Users.Find(id);
            if (wa_users == null)
            {
                return HttpNotFound();
            }
            return View(wa_users);
        }

        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Edit([Bind(Include = "UserID,UserName,Email,DisplayName,IsAdmin")] WA_Users wa_users, HttpPostedFileBase filebase)
        {
            if (ModelState.IsValid)
            {
                WA_Users change = db.WA_Users.Find(wa_users.UserID);
                if (Request.Files.Count > 0 && !String.IsNullOrEmpty(Request.Files[0].FileName))
                {
                    string path = "~/Content/images/avatar";
                    string pathToSave = Server.MapPath(path);
                    string filename = Path.GetFileName(Request.Files[0].FileName);
                    Request.Files[0].SaveAs(Path.Combine(pathToSave, filename));
                    change.Avatar = filename;
                }
                change.UserName = wa_users.UserName;
                change.Email = wa_users.Email;
                change.DisplayName = wa_users.DisplayName;
                change.IsAdmin = wa_users.IsAdmin;
                db.Entry(change).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wa_users);
        }

        // GET: /Users/Delete/5
        [AdminFilter]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Users wa_users = db.WA_Users.Find(id);
            if (wa_users == null)
            {
                return HttpNotFound();
            }
            return View(wa_users);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult DeleteConfirmed(int id)
        {
            WA_Users wa_users = db.WA_Users.Find(id);
            db.WA_Users.Remove(wa_users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [AdminFilter]
        public ActionResult QuickEdit(int? userID)
        {
            var user = db.WA_Users.Find(userID);
            return View(user);
        }
        [HttpPost]
        [AdminFilter]
        public ActionResult QuickEdit(int UserID, string UserName, string Email, string DisplayName)
        {
            WA_Users user = db.WA_Users.Find(UserID);
            if (user != null)
            {
                user.UserName = UserName;
                user.Email = Email;
                user.DisplayName = DisplayName;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [AdminFilter]
        public JsonResult SaveQuickEdit(int? userID, string userName, string email, string nameDisplay)
        {
            var user = db.WA_Users.Find(userID);
            if (user != null)
            {
                user.UserName = userName;
                user.Email = email;
                user.DisplayName = nameDisplay;
                user.Modified = DateTime.Now;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        [AdminFilter]
        public void Reset(int id)
        {
            WA_Users user = db.WA_Users.Find(id);
            if(user !=null)
            {
                MD5 pass = MD5.Create();
                user.Password = GetMd5Hash(pass,"123456");
                db.SaveChanges();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
