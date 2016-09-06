using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Quotes.DAL;
using Quotes.FrameworkExtension;
using Quotes.Models;
using System;

namespace Quotes.Controllers
{
    public class QuoteController : Controller
    {
        [CustomAuthorize]
        public ActionResult MyQuotes()
        {
            return View(QuoteDAL.FindUserQuotes(User.Identity.GetUserId<int>()));
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
            return View("SearchResultQuotes",QuoteDAL.FindQuote(searchText));
        }

        //Search Api
        public JsonResult Search(string searchText,string userName,string from, string to, int maxRecords = 1000,string order = "DESC")
        {
            DateTime? fromDate = null;
            DateTime? toDate = null;
            try
            {
                if(from != null)
                    fromDate = DateTime.Parse(from);
                if (to != null)
                    toDate = DateTime.Parse(to);
            }
            catch(Exception)
            {
                return Json(new { success = false, responseText = "Incorrect Date Format, should be : '2012-04-23 18:25:43.511' " }, JsonRequestBehavior.AllowGet);
            }
            
            
            //search quote in database and Load result page
            return Json(QuoteDAL.QuoteFilterList(searchText,userName,fromDate,toDate,maxRecords,order), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TagQuote(int quoteId,string tag)
        {
            //Increment a Tag on the quote (like, dislike or any other tag), return the json result of the request.
            return Json(new { success = QuoteDAL.TagQuote(new QuoteModel() { QuoteId = quoteId, UserId = User.Identity.GetUserId<int>() }, tag), responseText = "" }, JsonRequestBehavior.AllowGet);
        }


    }
}