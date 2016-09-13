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

            return PartialView("_Announcements", new List<AnnouncementModel>());
        }


        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create(AnnouncementModel model)
        {
            AnnouncementDAL.SaveAnnouncement(model);
            return RedirectToAction("Index","Home");
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public void Update(AnnouncementModel model)
        {
            AnnouncementDAL.SaveAnnouncement(model);
            
        }

    }
}