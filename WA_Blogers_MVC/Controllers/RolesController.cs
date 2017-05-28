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
//    public class RolesController : Controller
//    {
//        private WA_BlogerEntities db = new WA_BlogerEntities();

//        // GET: /Roles/
//        public ActionResult Index()
//        {
//            return View(db.WA_Roles.ToList());
//        }

//        // GET: /Roles/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Roles wa_roles = db.WA_Roles.Find(id);
//            if (wa_roles == null)
//            {
//                return HttpNotFound();
//            }
//            return View(wa_roles);
//        }

//        // GET: /Roles/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: /Roles/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include="RoleID,RoleName,Description")] WA_Roles wa_roles)
//        {
//            if (ModelState.IsValid)
//            {
//                db.WA_Roles.Add(wa_roles);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            return View(wa_roles);
//        }

//        // GET: /Roles/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Roles wa_roles = db.WA_Roles.Find(id);
//            if (wa_roles == null)
//            {
//                return HttpNotFound();
//            }
//            return View(wa_roles);
//        }

//        // POST: /Roles/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include="RoleID,RoleName,Description")] WA_Roles wa_roles)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(wa_roles).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            return View(wa_roles);
//        }

//        // GET: /Roles/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            WA_Roles wa_roles = db.WA_Roles.Find(id);
//            if (wa_roles == null)
//            {
//                return HttpNotFound();
//            }
//            return View(wa_roles);
//        }

//        // POST: /Roles/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            WA_Roles wa_roles = db.WA_Roles.Find(id);
//            db.WA_Roles.Remove(wa_roles);
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
