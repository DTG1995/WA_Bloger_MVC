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
        public ActionResult Index(string searchString, string sortOrder, string currentFilter, string numDisplay, int? page)
        {
            //Hiển thị
            int numberDisplay;
            // kiểm tra numDisplay truyền vào là tất cả
            if (numDisplay != null && numDisplay.Equals("Tất cả"))
            {
                numberDisplay = db.WA_Blogs.Count();
                ViewBag.numDisplay = "Tất cả";
                ViewBag.TemnumDisplay = "Tất cả";
            }
            else
            {
                // b =a.b
                if (numDisplay != null)
                {
                    numberDisplay = ViewBag.TemnumDisplay = ViewBag.numDisplay = int.Parse(numDisplay);
                }
                else
                {
                    numberDisplay = ViewBag.numDisplay = 5;
                }
            }
            if (page != null)
            {
                ViewBag.Page = page;
            }
            int pagetemp = page ?? 1;// nếu page == null thì lấy = 1; khác null lấy page

            //lấy tất cả các bài viết
            var wa_blogs = db.WA_Blogs.ToList();

            var blog = from b in db.WA_Blogs select b;
            ViewBag.CurrentSort = sortOrder;

            //search
            if (!String.IsNullOrEmpty(searchString))
            {
                blog = blog.Where(s => s.Name.Contains(searchString));
            }

            //sort
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" :"";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "parent_sc" : "";
            switch (sortOrder)
            {
                case "name_asc":
                    blog = blog.OrderByDescending(b => b.Name);
                    break;
                case "parent_asc":
                    blog = blog.OrderByDescending(b => b.Parent);
                    break;
                default:
                    blog = blog.OrderBy(b => b.Name);
                    break;
            }

            //pagelist
            if (searchString != null) page = 1;
            else searchString = currentFilter;
            ViewBag.CurrentFilter = searchString;
           
            int pageNumber = (page ?? 1);
            return View(blog.ToPagedList(pageNumber,numberDisplay));
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
        //[AdminFilter]
        public ActionResult Create()
        {
            List<SelectListItem> selectlist = new SelectList(db.WA_Blogs.Where(x => x.Active == true).ToList(), "BlogID", "Name").ToList();
            selectlist.Insert(0, new SelectListItem
            {
                Selected = true,
                Text = "",
                Value = null
            });
            ViewBag.Parent = selectlist.ToList();
                                  
            return View();
        }

        // POST: /Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="BlogID,Name,Parent,Order,Active")] WA_Blogs wa_blogs)
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
            List<SelectListItem> selectlist = new SelectList(db.WA_Blogs.Where(x => x.Active == true).ToList(), "BlogID", "Name").ToList();
            selectlist.Insert(0, new SelectListItem
            {
                Selected = true,
                Text = "",
                Value = null
            });
            ViewBag.Parent = selectlist.ToList();
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
        public ActionResult Edit([Bind(Include="BlogID,Name,Parent,Order,Active")] WA_Blogs wa_blogs)
        {
            if (ModelState.IsValid)
            {
                WA_Blogs change = db.WA_Blogs.Find(wa_blogs.BlogID);
                change.Name = wa_blogs.Name;
                change.Parent = wa_blogs.Parent;
                change.Active = wa_blogs.Active;

                db.Entry(change).State = EntityState.Modified;
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
        [NonAction]
        private void DeleteComments(WA_Comments comment)
        {
            var commentChil = comment.WA_Comments1.ToList();
            for (int i = 0; i < commentChil.Count; i++)
            {
                DeleteComments(commentChil[i]);
            }
            db.WA_Comments.Remove(comment);
            db.SaveChanges();
        }
        [NonAction]
        private void DeleteLoop(WA_Blogs blog)
        {
            var Blogs1 = blog.WA_Blogs1.ToList();
            for (int i = 0; i < Blogs1.Count; i++)
            {
                DeleteLoop(Blogs1[i]);
            }
            var Posts = blog.WA_Posts.ToList();
            for (int i = 0; i < Posts.Count; i++)
            {
                WA_Posts wa_posts = Posts[i];
                if (wa_posts != null)
                {
                    var comments = wa_posts.WA_Comments.ToList();
                    for (int j = 0; j < comments.Count; j++)
                    {
                        DeleteComments(comments[j]);
                    }
                    var likes = wa_posts.WA_Likes.ToList();
                    for (int j = 0; j < likes.Count; j++)
                    {
                        db.WA_Likes.Remove(likes[j]);
                        db.SaveChanges();
                    }
                    db.WA_Posts.Remove(wa_posts);
                    db.SaveChanges();
                }
            }
            
            db.WA_Blogs.Remove(blog);
            db.SaveChanges();
        }
        // POST: /Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public string DeleteConfirmed(int? id)
        {

            try
            {
                var Blog = db.WA_Blogs.Find(id);
                DeleteLoop(Blog);
                return "true";
            }
            catch
            {
                return "false";
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
            ViewBag.Parent = new SelectList(db.WA_Blogs.Where(x => x.Active == true).ToList(), "BlogID", "Name");
            var blog = db.WA_Blogs.Find(blogID);
            return View(blog);
        }

        [HttpPost]
        public ActionResult QuickEdit([Bind(Include="BlogID,Name,Parent,Order")] WA_Blogs wa_blogs,HttpPostedFileBase filebase)
        {
            
            if (ModelState.IsValid)
            {
                WA_Blogs change = db.WA_Blogs.Find(wa_blogs.BlogID);
                change.Name = wa_blogs.Name;
                change.Parent = wa_blogs.Parent;
                change.Order = wa_blogs.Order;

                db.Entry(change).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wa_blogs);          
            
        }

        
        public string SaveQuickEdit(int? blogID, string name, int? parent, int? order, string active)
        {
            bool Active = !String.IsNullOrEmpty(active);
            var blog = db.WA_Blogs.Find(blogID);
            if (blog != null)
            {
                blog.Name = name;
                blog.Parent = parent;
                blog.Order =order;
                blog.Active = Active;
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (blog != null)
            {
                return "Lưu thành công!";
            }
            return "Lỗi";
        }
        public string SetActive(bool active, int blogID)
        {
            var blog = db.WA_Blogs.Find(blogID);
            blog.Active = active;
            db.Entry(blog).State = EntityState.Modified;
            return db.SaveChanges().ToString();
        }
        

    }
}
