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
    public class LikesController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Likes/
        public ActionResult Index()
        {
            var wa_likes = db.WA_Likes.Include(w => w.WA_Posts).Include(w => w.WA_Users);
            return View(wa_likes.ToList());
        }

        // GET: /Likes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Likes wa_likes = db.WA_Likes.Find(id);
            if (wa_likes == null)
            {
                return HttpNotFound();
            }
            return View(wa_likes);
        }

        // GET: /Likes/Create
        public ActionResult Create()
        {
            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title");
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName");
            return View();
        }

        // POST: /Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="PostID,Author,IsLike")] WA_Likes wa_likes)
        {
            if (ModelState.IsValid)
            {
                db.WA_Likes.Add(wa_likes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_likes.PostID);
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_likes.Author);
            return View(wa_likes);
        }

        // GET: /Likes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Likes wa_likes = db.WA_Likes.Find(id);
            if (wa_likes == null)
            {
                return HttpNotFound();
            }
            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_likes.PostID);
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_likes.Author);
            return View(wa_likes);
        }

        // POST: /Likes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="PostID,Author,IsLike")] WA_Likes wa_likes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wa_likes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_likes.PostID);
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_likes.Author);
            return View(wa_likes);
        }

        // GET: /Likes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Likes wa_likes = db.WA_Likes.Find(id);
            if (wa_likes == null)
            {
                return HttpNotFound();
            }
            return View(wa_likes);
        }

        // POST: /Likes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WA_Likes wa_likes = db.WA_Likes.Find(id);
            db.WA_Likes.Remove(wa_likes);
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
