using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Quotes.Models;

namespace Quotes.Controllers
{
    public class UserRolesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CustomUserRoles
        public ActionResult Index(int? userId)
        {
            var list = db.CustomUserRoles.ToList();
            var userRoles = list.Select(item => new UserRoleViewModel()
            {
                RoleId = db.Roles.Find(item.RoleId).Id,
                RoleLabel = db.Roles.Find(item.RoleId).Name,
                UserId = db.Users.Find(item.UserId).Id,
                UserName = db.Users.Find(item.UserId).UserName
            }).ToList();

            return View(userId== null ? userRoles : userRoles.Where(x=>x.UserId == userId));
        }

        // GET: CustomUserRoles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId")] CustomUserRole customUserRole)
        {
            if (ModelState.IsValid)
            {
                db.CustomUserRoles.Add(customUserRole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customUserRole);
        }

        // GET: CustomUserRoles/Edit/5
        public ActionResult Edit(int? roleId, int? userId)
        {
            if (roleId == null || userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomUserRole customUserRole = db.CustomUserRoles.Find(userId,roleId);
            if (customUserRole == null)
            {
                return HttpNotFound();
            }
            return View(customUserRole);
        }

        // POST: CustomUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId")] CustomUserRole customUserRole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customUserRole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customUserRole);
        }

        // GET: CustomUserRoles/Delete/5
        public ActionResult Delete(int? roleId, int? userId)
        {
            if (roleId == null || userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomUserRole customUserRole = db.CustomUserRoles.Find(userId, roleId);
            if (customUserRole == null)
            {
                return HttpNotFound();
            }
            return View(customUserRole);
        }

        // POST: CustomUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int roleId, int userId)
        {
            CustomUserRole customUserRole = db.CustomUserRoles.Find(userId, roleId);
            db.CustomUserRoles.Remove(customUserRole);
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
