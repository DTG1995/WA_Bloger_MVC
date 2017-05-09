﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Filter;
using WA_Blogers_MVC.Models;

namespace WA_Blogers_MVC.Controllers
{
    public class BlogsController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Blogs/
        public ActionResult Index()
        {
            return View(db.WA_Blogs.ToList());
        }

        // GET: /Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Blogs wa_blogs = db.WA_Blogs.Find(id);
            if (wa_blogs == null)
            {
                return HttpNotFound();
            }
            return View(wa_blogs);
        }

        // GET: /Blogs/Create
        [AdminFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BlogID,Name,Parent,Active")] WA_Blogs wa_blogs)
        {
            if (ModelState.IsValid)
            {
                db.WA_Blogs.Add(wa_blogs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wa_blogs);
        }

        // GET: /Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Blogs wa_blogs = db.WA_Blogs.Find(id);
            if (wa_blogs == null)
            {
                return HttpNotFound();
            }
            return View(wa_blogs);
        }

        // POST: /Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BlogID,Name,Parent,Active")] WA_Blogs wa_blogs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wa_blogs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wa_blogs);
        }

        // GET: /Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Blogs wa_blogs = db.WA_Blogs.Find(id);
            if (wa_blogs == null)
            {
                return HttpNotFound();
            }
            return View(wa_blogs);
        }

        // POST: /Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WA_Blogs wa_blogs = db.WA_Blogs.Find(id);
            db.WA_Blogs.Remove(wa_blogs);
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

        public ActionResult MainMenu()
        {
            var model = db.WA_Blogs.Where(x => x.Parent == null && x.Active == true).OrderBy(x => x.Order).ToList();
            return PartialView(model);
        }

        //GET: //Blogs/Name-Blog
        public ActionResult ViewBlogs(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WA_Blogs wa_blogs = db.WA_Blogs.FirstOrDefault(x=>x.BlogID ==id);
            if (wa_blogs == null)
            {
                return HttpNotFound();
            }
            //string titleEncode = UrlEncode.ToFriendlyUrl(wa_blogs.Name);
            return View(wa_blogs.WA_Blogs1);
        }
    }
}
