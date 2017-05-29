using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using WA_Blogers_MVC.Models;
using PagedList;
using PagedList.Mvc;
using WA_Blogers_MVC.Filter;
namespace WA_Blogers_MVC.Controllers
{
    public class PostsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private WA_BlogerEntities db = new WA_BlogerEntities();
        /// <summary>
        /// Hiển thị giao diện quản lý Post, tìm kiếm, sắp xếp, ...
        /// </summary>
        /// <param name="searchString">tìm kiếm theo tiêu đề</param>
        /// <param name="numDisplay">số dòng hiển thị trên một trang</param>
        /// <param name="page">số trang truyền vào</param>
        /// <param name="sorttitle">sắp xếp theo tiêu đề</param>
        /// <param name="sortcreate">sắp xếp theo ngày tạo</param>
        /// <param name="sortactive">sắp xếp theo hoạt động</param>
        /// <param name="sortauthor">sắp xếp theo tác giả</param>
        /// <returns></returns>
        [AdminFilter]
        public ActionResult Index(string searchString, 
            string numDisplay, int? page, 
            string sorttitle = "", string sortcreate = "", 
            string sortactive = "", string sortauthor = "")
        {
            //
            int numberDisplay;
            // kiểm tra numDisplay truyền vào là tất cả
            if (numDisplay != null && numDisplay.Equals("Tất cả"))
            {
                numberDisplay = db.WA_Posts.Count();
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
                    numberDisplay = ViewBag.numDisplay = 10;
                }
            }
            if (page!=null )                                  
            {
                ViewBag.Page = page;
            }
            int pagetemp = page ?? 1;// nếu page == null thì lấy = 1; khác null lấy page
            //var wa_posts = from p in db.WA_Posts select p;

            //lấy tất cả các bài viết
            var wa_posts = db.WA_Posts.ToList();

            // tìm kiếm
            ViewBag.searchString = searchString;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                wa_posts = wa_posts.Where(s => s.Title != null && (s.Title).ToUpper().Contains(searchString.ToUpper())).ToList();
            }
            
           // kiểm tra active class trong menu
            ViewBag.PostActive = "active";

            // sắp xếp
            // StringComparison.OrdinalIgnoreCase : bỏ qua trường hợp viết thường viết hoa
            //sort title
            if (sorttitle.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderBy(n=>n.Title).ToList();
                ViewBag.SortTitle = "desc";
                ViewBag.TempSortTitle = sorttitle;
            }
            if (sorttitle.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderByDescending(n => n.Title).ToList();
                ViewBag.SortTitle = "asc";
                ViewBag.TempSortTitle = sorttitle;
            }
            // sort create 
            if (sortcreate.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderBy(n => n.Created).ToList();
                ViewBag.SortCreate = "desc";
                ViewBag.TempSortCreate = sortcreate;
            }
            if (sortcreate.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderByDescending(n => n.Created).ToList();
                ViewBag.SortCreate = "asc";
                ViewBag.TempSortCreate = sortcreate;
            }
            // sort active 
            if (sortactive.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderBy(n => n.Active).ToList();
                ViewBag.SortActive = "desc";
                ViewBag.TempSortActive = sortactive;
            }
            if (sortactive.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderByDescending(n => n.Active).ToList();
                ViewBag.SortActive = "asc";
                ViewBag.TempSortActive = sortactive;
            }
            // sort author 
            if (sortauthor.Equals("asc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderBy(n => n.Author).ToList();
                ViewBag.SortAuthor = "desc";
                ViewBag.TempSortAuthor = sortauthor;
            }
            if (sortauthor.Equals("desc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderByDescending(n => n.Author).ToList();
                ViewBag.SortAuthor = "asc";
                ViewBag.TempSortAuthor = sortauthor;
            }

            wa_posts.ForEach(n =>
            {
                n.Description = n.Description == null ? "" : (n.Description.Length > 20 ? n.Description.Substring(0, 20) + "..." : n.Description);
                n.ContentPost = n.ContentPost == null ? "" : (n.ContentPost.Length > 20 ? n.ContentPost.Substring(0, 20) + "..." : n.ContentPost);
            });
            return View(wa_posts.ToPagedList(pagetemp, numberDisplay));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PostID"></param>
        /// <returns></returns>
        [AdminFilter]
        public ActionResult QuickEdit(int PostID)
        {
            ViewBag.ListBlogs = db.WA_Blogs.Where(x=>x.Parent==null && x.Active).ToList();
            var post = db.WA_Posts.FirstOrDefault(n=>n.PostID==PostID);
            return View(post);
        }
        [HttpPost]
        [AdminFilter]
        public ActionResult QuickEdit([Bind(Include = "PostID,Title,Description,ContentPost,Active")] WA_Posts wa_posts, int[] Blogs, HttpPostedFileBase filebase)
        {
            
            if (ModelState.IsValid)
            {
                if(Blogs==null || Blogs.Count()==0)
                    return View("Edit", wa_posts);
                WA_Posts change = db.WA_Posts.Find(wa_posts.PostID);
                change.Title = wa_posts.Title;
                change.Description = wa_posts.Description;
                change.ContentPost = wa_posts.ContentPost;
                change.Active = wa_posts.Active;
                change.WA_Blogs = new List<WA_Blogs>();

                //Save pic
                string path = "~/Content/images/thuvien";
                if (Request.Files[0] != null)
                {
                    string tg = DateTime.Now.ToString("ddMMyyyy_");
                    string pathToSave = Server.MapPath(path);
                    string filename =  tg+Path.GetFileName(Request.Files[0].FileName);
                    change.Picture = Path.Combine(path, filename);
                    Request.Files[0].SaveAs( Path.Combine(pathToSave, filename));
                }

                foreach (var item in Blogs)
                {
                    change.WA_Blogs.Add(db.WA_Blogs.Find(item));
                }
                db.Entry(change).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View("Edit", wa_posts);
        }
        [HttpPost]

        [AdminFilter]
        public string SaveQuickEdit(int? PostID, string title, string description, string active, string content)
        {
            bool Active = !String.IsNullOrEmpty(active);
            var post = db.WA_Posts.Find(PostID);
            if (post != null)
            {
                post.Title = title;
                post.Active = Active;
                post.Description = description;
                post.ContentPost = content.ToString() ;
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (post!=null)
            {
                return "Lưu thành công!";
            }
            return "Lỗi";
        }
        // GET: /Posts/Details/5
        [AdminFilter]
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
        [AdminFilter]
        public ActionResult DetailsAjax(int? id)
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
        [AdminFilter]
        public ActionResult DeleteAjax(int? id)
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
        [AdminFilter]
        public ActionResult Create()
        {
            ViewBag.ListBlogs = db.WA_Blogs.Where(x => x.Parent == null).ToList();
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName");
            return View();
        }

        // POST: /Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Create([Bind(Include = "PostID,Title,Description,ContentPost,Picture,UseDescription,Active")] WA_Posts wa_posts, int[] Blogs, HttpPostedFileBase filebase)
        {
            if (ModelState.IsValid)
            {
                if (Blogs == null || Blogs.Count()==0)
                {
                    return View("Error");
                }
                wa_posts.Created = DateTime.Now;
                wa_posts.Active = false;
                wa_posts.Seen = 0;
                wa_posts.Author = ((WA_Users)Session["UserLogin"]).UserID;
                for (int i = 0; i < Blogs.Count(); i++)
                {
                    wa_posts.WA_Blogs.Add(db.WA_Blogs.Find(Blogs[i]));
                }
                string path = "~/Content/images/thuvien";
                if (Request.Files[0] != null)
                {
                    string tg = DateTime.Now.ToString("ddMMyyyy_");
                    string pathToSave = Server.MapPath(path);
                    string filename = tg + Path.GetFileName(Request.Files[0].FileName);
                    wa_posts.Picture = Path.Combine(path, filename);
                    Request.Files[0].SaveAs(Path.Combine(pathToSave, filename));
                }
                db.WA_Posts.Add(wa_posts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // GET: /Posts/Edit/5
        [AdminFilter]
        public ActionResult Edit(int? id, string title)
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
            ViewBag.ListBlogs = db.WA_Blogs.Where(x => x.Parent == null && x.Active).ToList();
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // POST: /Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [AdminFilter]
        public ActionResult Edit([Bind(Include = "PostID,Title,Description,ContentPost,Picture,UseDescription,Active")] WA_Posts wa_posts, HttpPostedFileBase filebase)
        {
            if (ModelState.IsValid)
            {
                WA_Posts change = db.WA_Posts.Find(wa_posts.PostID);
                change.Title = wa_posts.Title;
                change.Description = wa_posts.Description;
                change.ContentPost = wa_posts.ContentPost;
                change.UseDescription = wa_posts.UseDescription;
                change.Active = wa_posts.Active;
                string path = "~/Content/images/thuvien";
                if (!string.IsNullOrEmpty(Request.Files[0].FileName ))
                {
                    string tg = DateTime.Now.ToString("ddMMyyyy_");
                    string pathToSave = Server.MapPath(path);
                    string filename = tg + Path.GetFileName(Request.Files[0].FileName);
                    wa_posts.Picture = Path.Combine(path, filename);
                    change.Picture = wa_posts.Picture;
                    Request.Files[0].SaveAs(Path.Combine(pathToSave, filename));
                }
                db.Entry(change).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Author = new SelectList(db.WA_Users, "UserID", "UserName", wa_posts.Author);
            return View(wa_posts);
        }

        // GET: /Posts/Delete/5
        [AdminFilter]
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
        
        // POST: /Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [AdminFilter]
        public ActionResult DeleteConfirmed(int id, string title)
        {
            WA_Posts wa_posts = db.WA_Posts.Single(n=>n.PostID==id);
            if (wa_posts != null)
            {
                var blogs = wa_posts.WA_Blogs.ToList();
                for (int i = 0; i < blogs.Count; i++)
                {
                    WA_Blogs blog = blogs[i];
                    blog.WA_Posts.Remove(wa_posts);
                    db.Entry(blog).State = EntityState.Modified;
                    db.SaveChanges();
                }
                var comments = wa_posts.WA_Comments.ToList();
                for (int i = 0; i < comments.Count; i++)
                {
                    DeleteComments(comments[i]);
                }
                var likes = wa_posts.WA_Likes.ToList();
                for (int i = 0; i < likes.Count; i++)
                {
                    db.WA_Likes.Remove(likes[i]);
                    db.SaveChanges();
                }
                db.WA_Posts.Remove(wa_posts);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult ViewPost(int? id)
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
            wa_posts.Seen++;
            db.Entry(wa_posts).State = EntityState.Modified;
            db.SaveChanges();
            return View(wa_posts);
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
