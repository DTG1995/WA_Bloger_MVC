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
using PagedList.Mvc;
namespace WA_Blogers_MVC.Controllers
{
    public class PostsController : Controller
    {
        private WA_BlogerEntities db = new WA_BlogerEntities();
        public ActionResult Index(string searchString, string numDisplay, string sortOrder="", int page = 1)
        {
            int numberDisplay;
            if (numDisplay != null && numDisplay.Equals("Tất cả"))
            {
                numberDisplay = db.WA_Posts.Count();
                ViewBag.numDisplay = "Tất cả";
            }
            else
            {
                numberDisplay = ViewBag.numDisplay = numDisplay == null ? 10 : int.Parse(numDisplay);
            }
            
            ViewBag.Page = page;
            //var wa_posts = from p in db.WA_Posts select p;
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
            if (String.IsNullOrEmpty(sortOrder))
            {
                 wa_posts = wa_posts.OrderByDescending(n=>n.Created).ToList();
            }
           
            if (sortOrder.Equals("title_asc",StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderBy(n=>n.Title).ToList();
                ViewBag.Sort = "title_desc";
                ViewBag.NSort = sortOrder;
            }
            if (sortOrder.Equals("title_desc", StringComparison.OrdinalIgnoreCase))
            {
                wa_posts = wa_posts.OrderByDescending(n => n.Title).ToList();
                ViewBag.Sort = "title_asc";
                ViewBag.NSort = sortOrder;
            }

            wa_posts.ForEach(n =>
            {
                n.Description = n.Description == null ? "" : (n.Description.Length > 20 ? n.Description.Substring(0, 20) + "..." : n.Description);
                n.ContentPost = n.ContentPost == null ? "" : (n.ContentPost.Length > 20 ? n.ContentPost.Substring(0, 20) + "..." : n.ContentPost);
            });
            return View(wa_posts.ToPagedList(page,numberDisplay));
        }
        
        public ActionResult QuickEdit(int PostID)
        {
            var post = db.WA_Posts.FirstOrDefault(n=>n.PostID==PostID);
            return View(post);
        }
        [HttpPost]
        [ValidateInput(false)]
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
        public ActionResult Create([Bind(Include="PostID,Title,Description,ContentPost,Author,Picture,UseDescription,Active")] WA_Posts wa_posts)
        {
            if (ModelState.IsValid)
            {
                wa_posts.Created = DateTime.Now;
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
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WA_Posts wa_posts = db.WA_Posts.Single(n=>n.PostID==id);
            if (wa_posts != null)
            {
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
