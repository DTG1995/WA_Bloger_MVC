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
    public class PostsController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Posts/
        public ActionResult Index()
        {
            var wa_posts = db.WA_Posts.Include(w => w.WA_Users);
            return View(wa_posts.ToList());
        }

        // GET: /Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Posts wa_posts = db.WA_Posts.Find(id);
            if (wa_posts == null)
            {
                return HttpNotFound();
            }
            return View(wa_posts);
        }

        // GET: /Posts/Create
        public ActionResult Create()
        {
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName");
            return View();
        }

        // POST: /Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PostID,Title,Description,ContentPost,Created,Author,Picture,UseDescription,Active")] WA_Posts wa_posts)
        {
            if (ModelState.IsValid)
            {
                db.WA_Posts.Add(wa_posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // GET: /Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Posts wa_posts = db.WA_Posts.Find(id);
            if (wa_posts == null)
            {
                return HttpNotFound();
            }
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // POST: /Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PostID,Title,Description,ContentPost,Created,Author,Picture,UseDescription,Active")] WA_Posts wa_posts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wa_posts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // GET: /Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Posts wa_posts = db.WA_Posts.Find(id);
            if (wa_posts == null)
            {
                return HttpNotFound();
            }
            return View(wa_posts);
        }

        // POST: /Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WA_Posts wa_posts = db.WA_Posts.Find(id);
            db.WA_Posts.Remove(wa_posts);
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
