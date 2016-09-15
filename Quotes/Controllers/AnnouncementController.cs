using System.Net;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Quotes.FrameworkExtension;
using Quotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quotes.DAL;

namespace Quotes.Controllers
{
    public class AnnouncementController : Controller
    {
        // GET: Announcement

        public ActionResult List()
        {

            return PartialView("_Announcements", AnnouncementDAL.GetList());
        }


        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public JsonResult Create()
        {
            try
            {
                AnnouncementDAL.SaveAnnouncement(new AnnouncementModel()
                {
                    Author = User.Identity.Name,
                    RawHtml = "New Text",
                    Title = "New Title"
                }, User.Identity.GetUserId<int>());
                
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
           
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public JsonResult Update(AnnouncementModel model)
        {
            if(model == null)
                return Json(new { success = false, message = "Announcement Model data could not be retrieved." }, JsonRequestBehavior.AllowGet);

            try
            {
                model.RawHtml = WebUtility.UrlDecode(model.RawHtml);
                AnnouncementDAL.SaveAnnouncement(model, User.Identity.GetUserId<int>());
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {

            try
            {
                AnnouncementDAL.Delete(id);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e.Message }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);

        }

    }
}