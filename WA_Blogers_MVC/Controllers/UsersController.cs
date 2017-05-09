using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;

namespace WA_Blogers_MVC.Controllers
{
    public class UsersController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Users/
        public ActionResult Index()
        {
            return View(db.WA_Users.ToList());
        }

        // GET: /Users/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserID,UserName,Email,Password,DisplayName,Created,Modified,Avatar,LastLogin,IPLast,IPCreated")] WA_Users wa_users)
        {
            if (ModelState.IsValid)
            {
                db.WA_Users.Add(wa_users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wa_users);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(string id)
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
        public ActionResult Edit([Bind(Include="UserID,UserName,Email,Password,DisplayName,Created,Modified,Avatar,LastLogin,IPLast,IPCreated")] WA_Users wa_users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wa_users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wa_users);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(string id)
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
        public ActionResult DeleteConfirmed(string id)
        {
            WA_Users wa_users = db.WA_Users.Find(id);
            db.WA_Users.Remove(wa_users);
            db.SaveChanges();
            return RedirectToAction("Index");
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
