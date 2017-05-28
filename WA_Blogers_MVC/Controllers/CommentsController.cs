//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using WA_Blogers_MVC.Models;

//namespace WA_Blogers_MVC.Controllers
//{
//    public class CommentsController : Controller
//    {
//        private WA_BlogerEntities db = new WA_BlogerEntities();

//        // GET: /Comments/
//        public ActionResult Index()
//        {
//            var wa_comments = db.WA_Comments.Include(w => w.WA_Posts).Include(w => w.WA_Users);
//            return View(wa_comments.ToList());
//        }

//        // GET: /Comments/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Comments wa_comments = db.WA_Comments.Find(id);
//            if (wa_comments == null)
//            {
//                return HttpNotFound();
//            }
//            return View(wa_comments);
//        }

//        // GET: /Comments/Create
//        public ActionResult Create()
//        {
//            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title");
//            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName");
//            return View();
//        }

//        // POST: /Comments/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include="CommentID,ContenComment,PostID,Author,Created,Parent")] WA_Comments wa_comments)
//        {
//            if (ModelState.IsValid)
//            {
//                db.WA_Comments.Add(wa_comments);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_comments.PostID);
//            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_comments.Author);
//            return View(wa_comments);
//        }

//        // GET: /Comments/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Comments wa_comments = db.WA_Comments.Find(id);
//            if (wa_comments == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_comments.PostID);
//            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_comments.Author);
//            return View(wa_comments);
//        }

//        // POST: /Comments/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include="CommentID,ContenComment,PostID,Author,Created,Parent")] WA_Comments wa_comments)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(wa_comments).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.PostID = new SelectList(db.WA_Posts, "PostID", "Title", wa_comments.PostID);
//            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_comments.Author);
//            return View(wa_comments);
//        }

//        // GET: /Comments/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Comments wa_comments = db.WA_Comments.Find(id);
//            if (wa_comments == null)
//            {
//                return HttpNotFound();
//            }
//            return View(wa_comments);
//        }

//        // POST: /Comments/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            WA_Comments wa_comments = db.WA_Comments.Find(id);
//            db.WA_Comments.Remove(wa_comments);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
