using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Quotes.DAL;
using Quotes.FrameworkExtension;
using Quotes.Models;
using System;
using System.Linq;

namespace Quotes.Controllers
{
    public class QuoteController : Controller
    {
        [CustomAuthorize]
        public ActionResult MyQuotes()
        {
            var userId = User.Identity.GetUserId<int>();
            ViewData["lastPostDate"] = QuoteDAL.CheckLastUserPostDate(userId);
            return View(QuoteDAL.FindUserQuotes(userId));
        }

        [HttpPost]
        public ActionResult PostQuote(string text)
        {
            //Save new quote in database
            QuoteDAL.SaveQuote(new QuoteModel
            {
                QuoteText = text,
                UserId = User.Identity.GetUserId<int>()
            });

            //Refresh page
            return RedirectToAction("MyQuotes");
        }

        [HttpPost]
        public ActionResult SearchQuote(string searchText)
        {
            //search quote in database and Load result page
            return View("SearchResult",QuoteDAL.FindQuotes(searchText));
        }

        //Search Api
        public JsonResult Search(string searchText,string userName,string from, string to, int maxRecords = 1000,string order = "DESC")
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            var result = new List<UserQuoteModel>();

            try
            {
                if(from != null)
                    fromDate = DateTime.Parse(from);
                if (to != null)
                    toDate = DateTime.Parse(to);

                result = QuoteDAL.QuoteFilterList(searchText, userName, fromDate, toDate, maxRecords, order);
            }
            catch(Exception e)
            {
                return Json(new { success = false, responseText = e });
            }
            
            
            //search quote in database and Load result page
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method to return Calendar model Json for the calendar view of quotes
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="searchText"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="maxRecords"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public JsonResult SearchForCalendar(string userName, string searchText = null, string from=null, string to=null, int maxRecords = 1000, string order = "DESC")
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            var calendar= new CalendarModel();
            try
            {
                if (from != null)
                    fromDate = DateTime.Parse(from);
                if (to != null)
                    toDate = DateTime.Parse(to);


                var quoteList = QuoteDAL.QuoteFilterList(searchText, userName, fromDate, toDate, maxRecords, order);
                calendar = new CalendarModel() { result = new List<CalendarItem>() };
                foreach (var item in quoteList)
                {
                    var milli = Math.Floor( (item.Quote.OriginalDate - new DateTime(1970, 1, 1)).TotalMilliseconds).ToString();
                    calendar.result.Add(new CalendarItem() { id = item.Quote.QuoteId.ToString(), title = item.Quote.QuoteText, start = milli, end = milli, type = "event-important", url = "http://www.example.com" });
                }
            }
            catch (Exception)
            {
                //return json result in error
                calendar.success = 0;
                return Json(calendar, JsonRequestBehavior.AllowGet);
            }

            calendar.success = 1;
            var ouput =JsonConvert.SerializeObject( calendar);
            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
            //return json result in success
            return new JsonResult
            {
                Data = json_serializer.DeserializeObject(ouput),
                JsonRequestBehavior =  JsonRequestBehavior.AllowGet
            };
        }

        [CustomAuthorize]
        public ActionResult Calendar()
        {

            return View();
        }

        [CustomAuthorize]
        public JsonResult MyCalendar()
        {

            return SearchForCalendar(User.Identity.Name);
        }

        [HttpPost]
        [CustomAuthorize]
        public JsonResult TagQuote(int quoteId,string tag)
        {
            //Increment a Tag on the quote (like, dislike or any other tag), return the json result of the request.
            return Json(new { success = QuoteDAL.TagQuote(new QuoteModel() { QuoteId = quoteId, UserId = User.Identity.GetUserId<int>() }, tag), responseText = "" }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AdminQuoteList()
        {
            return View(QuoteDAL.FindQuotes("").Select(x=> new UserQuoteViewModel(x)).ToList());
        }



    }
}