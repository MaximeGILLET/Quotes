using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Provider;
using Quotes.FrameworkExtension;
using Quotes.Models;
using Quotes.DAL;
using System.Web.Security;

namespace Quotes.Controllers
{

    public class UsersController : Controller
    {

        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();

        public UsersController()
        {


        }

        public UsersController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: UserModels
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: UserModels/Details/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            var userDetail = new UserDetailModel(userModel);
            userDetail.assignedRoles = new List<string>(UserManager.GetRoles(userDetail.Id));
            return View(userDetail);
        }

        // GET: UserModels/Create
       [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "Id,UserName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,ProfileIconPath")] ApplicationUser userModel)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(userModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userModel);
        }

        // GET: UserModels/Edit/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            var userDetail = new UserDetailModel(userModel);
            userDetail.assignedRoles = new List<string>(UserManager.GetRoles(userDetail.Id));
            return View(userDetail);
        }

        // POST: UserModels/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "Id,UserName,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount")] ApplicationUser userModel)
        {
            //TODO carefull with the editings, if some values are missing those will erase the value in database!!
            if (ModelState.IsValid)
            {
                db.Entry(userModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(new UserDetailModel(userModel));
        }

        // GET: UserModels/Delete/5
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var userModel = db.Users.Find(id);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            var userModel = db.Users.Find(id);
            db.Users.Remove(userModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult Top()
        {
            try
            {

                var users = UserDAL.GetTopUsers();
                return Json(new { success = true, users = users.AllTimeUsers, monthUsers = users.MonthUsers, weekUsers = users.WeekUsers }, JsonRequestBehavior.AllowGet);
                
            }
            catch (Exception e)
            {
                return Json(new { success = true, message = e.Message }, JsonRequestBehavior.AllowGet);

            }
        }

        public JsonResult LastRegistered()
        {
            List<LastRegisterUserViewModel> userList = null;
            try
            {
                userList = UserDAL.LastRegisteredUsers();
                foreach (var user in userList)
                {
                    var days = Math.Floor((DateTime.Now - user.RegisterDate).TotalDays);


                    if (days > 0)
                    {
                        user.Label = days + " Day(s) ago.";
                        continue;
                    }
                    else
                    {

                        var hours = Math.Floor((DateTime.Now - user.RegisterDate).TotalHours);
                        if (hours > 0)
                        {
                            user.Label = hours + " Hour(s) ago.";
                            continue;
                        }
                        else
                        {
                            var minutes = Math.Floor((DateTime.Now - user.RegisterDate).TotalMinutes);
                            if (minutes > 0)
                            {
                                user.Label = minutes + " Minute(s) ago.";
                            }
                            else
                            {
                                user.Label = " Just now.";
                            }
                        }
                   
                    }
                     
                }
            }
            catch (Exception e)
            {
                return Json(new { success = false , message = e.Message}, JsonRequestBehavior.AllowGet);

            }
            return Json(new{ success= true, users=userList}, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize]
        public ActionResult Profile(string username)
        {
            if (String.IsNullOrEmpty(username) || username == User.Identity.Name)
            {
                ViewBag.IsMine = true;
            }
            ApplicationUser userModel = null;
            var userProfile = new UserProfileViewModel();
                        userProfile.CountryList = CountryDAL.CountryList;
            if (username == null)
            {
                userModel = db.Users.Find(User.Identity.GetUserId<int>());
                if (userModel == null)
                {
                    return HttpNotFound();
                }
                            userProfile.CountryList = CountryDAL.CountryList;
                userProfile.User = new UserDetailModel(userModel);
                userProfile.User.assignedRoles = new List<string>(UserManager.GetRoles(userProfile.User.Id));
                userProfile.CountryList = CountryDAL.CountryList;

                return View(userProfile);
            }
            userModel = UserManager.FindByName(username);
            if (userModel == null)
            {
                return HttpNotFound();
            }
            userProfile.User = new UserDetailModel(userModel);
            userProfile.User.assignedRoles = new List<string>(UserManager.GetRoles(userProfile.User.Id));
            return View(userProfile);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult CountrySave(string newRefCountry)
        {
            if (newRefCountry == null)
                return Json(new { success = false , message="New Country reference was not specified." }, JsonRequestBehavior.AllowGet);
            try
            {
                var userToUpdate = new ApplicationUser { Id = User.Identity.GetUserId<int>(),UserName = User.Identity.Name,RefCountry = newRefCountry };
                db.Users.Attach(userToUpdate);
                db.Entry(userToUpdate).Property(u => u.RefCountry).IsModified = true;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(new { success = false , message = e.Message}, JsonRequestBehavior.AllowGet);
            }
           

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult UploadImage(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Content/Users/Images"), User.Identity.GetUserId<int>()+".jpg");
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR : " + ex.Message;
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult Rename(string newName)
        {
            if (newName != null && newName != User.Identity.Name)
            {
                try
                {
                    //Check name availability
                    if (UserManager.FindByName(newName) != null)
                    {
                        return Json(new { success = false, message = "Username already in use" }, JsonRequestBehavior.AllowGet);
                    }
                    //Proceed the update
                    var userToUpdate = new ApplicationUser { Id = User.Identity.GetUserId<int>(), UserName = newName };
                    db.Users.Attach(userToUpdate);
                    db.Entry(userToUpdate).Property(u => u.UserName).IsModified = true;
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "Please chose a username different from the actual one"}, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize]
        public ActionResult MailBox()
        {

            return View();
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
