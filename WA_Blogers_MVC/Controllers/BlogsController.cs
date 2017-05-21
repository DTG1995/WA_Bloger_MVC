using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Filter;
using WA_Blogers_MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace WA_Blogers_MVC.Controllers
{
    public class BlogsController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();

        // GET: /Blogs/
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, int? page)
        {
            var blog = from b in db.WA_Blogs select b;
            ViewBag.CurrentSort = sortOrder;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                blog = blog.Where(s => s.Name.Contains(searchString));
            }

            //sort
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_asc" : sortOrder;
            switch (sortOrder)
            {
                case "parent":
                    blog = blog.OrderByDescending(b=>b.Parent);
                    break;
                case "name_desc":
                    blog = blog.OrderBy(b => b.Name);
                    break;
                default:
                    blog = blog.OrderByDescending(b => b.Name);
                    break;
            }

            //pagelist
            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
            int pageSize =3;
            int pageNumber = (page ?? 1);
            return View(blog.ToPagedList(pageNumber,pageSize));
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
        //[NonAction]
        //private void DeleteLoop(WA_Blogs blog)
        //{
        //    foreach (var item in blog.WA_Blogs1)
        //    {
        //        DeleteLoop(item);
        //    }
        //    foreach (var item in blog.WA_Posts)
        //    {
        //        db.WA_Posts.Remove(item);
        //    }
        //    db.WA_Blogs.Remove(blog);
        //    db.SaveChanges();
        //}
        // POST: /Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
        //    WA_Blogs wa_blogs = db.WA_Blogs.Find(id);
        //    DeleteLoop(wa_blogs);
        //    return RedirectToAction("Index");

            var Blog = db.WA_Blogs.Find(id);
            Blog.WA_Posts.ToList().ForEach(m => db.WA_Posts.Remove(m));
            db.WA_Blogs.Remove(Blog);
            db.SaveChanges();
            return new EmptyResult();
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

        [NonAction]
        private List<WA_Posts> GetAllPost(WA_Blogs blog)
        {
            List<WA_Posts> listpost = blog.WA_Posts.ToList();
            foreach (var item in blog.WA_Blogs1)
            {
                listpost.AddRange(GetAllPost(item));
            }
            return listpost;
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
            ViewBag.NameBlogs = wa_blogs.Name;
            var listPost = GetAllPost(wa_blogs).OrderByDescending(x => x.Created).Distinct().ToList();
            //string titleEncode = UrlEncode.ToFriendlyUrl(wa_blogs.Name);
            return View(listPost);
        }

        public ActionResult QuickEdit(int? blogID)
        {
            var blog = db.WA_Blogs.Find(blogID);
            return View(blog);
        }

        public JsonResult SaveQuickEdit(int? blogID, string name, bool? active)
        {
            bool Active = active ?? false;
            var blog = db.WA_Blogs.Find(blogID);
            object obj = new object();
            if (blog != null)
            {
                blog.Name = name;
                blog.Active = Active;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                obj = new { error = false, Name = name, Active = Active, IDBlog = blogID };
            }
            else
            {
                obj = new { error = true};
            }

            
            return Json(obj,JsonRequestBehavior.AllowGet);
            
        }

        

    }
}
