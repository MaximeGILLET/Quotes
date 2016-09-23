using System.Net;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
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


        public JsonResult Calendar(string userName = null, string searchText = null, string from = null,
            string to = null, int maxRecords = 1000, string order = "DESC")
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            var calendar = new CalendarModel();
            var startTime = new DateTime(1970, 1, 1);
            try
            {
                if (from != null)
                    fromDate = startTime.Add(TimeSpan.FromMilliseconds(Convert.ToDouble(from.Substring(0, 13))));
                if (to != null)
                    toDate = startTime.Add(TimeSpan.FromMilliseconds(Convert.ToDouble(to.Substring(0, 13))));

                var annList = AnnouncementDAL.AnnouncementFilterList(searchText, userName, fromDate, toDate, maxRecords, order);
                calendar = new CalendarModel() { result = new List<CalendarItem>() };
                foreach (var item in annList)
                {
                    var milli =
                        Math.Floor((item.CreationTime - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();
                    calendar.result.Add(new CalendarItem()
                    {
                        id = item.Id.ToString(),
                        title = item.Title +" - "+ item.Author,
                        start = milli,
                        end = milli,
                        type = "event-important",
                        url = ""
                    });
                }
            }
            catch (Exception e)
            {
                //return json result in error
                calendar.success = 0;
                return Json(calendar, JsonRequestBehavior.AllowGet);
            }

            calendar.success = 1;
            var ouput = JsonConvert.SerializeObject(calendar);
            var json_serializer = new JavaScriptSerializer();
            //return json result in success
            return new JsonResult
            {
                Data = json_serializer.DeserializeObject(ouput),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public ActionResult Archives()
        {

            return View();
        }

    }
}