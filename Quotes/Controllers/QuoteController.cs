using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Quotes.DAL;
using Quotes.Models;

namespace Quotes.Controllers
{
    public class QuoteController : Controller
    {

        public ActionResult MyQuotes()
        {

            return View(QuoteDAL.FindUserQuotes(User.Identity.GetUserId<int>()));
        }

        [HttpPost]
        public ActionResult PostQuote(string text)
        {
            //Save new quote in database
            QuoteDAL.SaveQuote(new QuoteModel() {QuoteText = text, UserId = User.Identity.GetUserId<int>()});

            //Refresh page
            return RedirectToAction("MyQuotes");
        }

        [HttpPost]
        public ActionResult SearchQuote(string searchText)
        {
            //search quote in database and Load result page
            return View("SearchResultQuotes",QuoteDAL.FindQuote(searchText));
        }

        [HttpPost]
        public JsonResult TagQuote(int quoteId,string tag)
        {
            //Increment a Tag on the quote (like, dislike or any other tag), return the json result of the request.
            return Json(new { success = QuoteDAL.TagQuote(new QuoteModel() { QuoteId = quoteId, UserId = User.Identity.GetUserId<int>() }, tag), responseText = "" }, JsonRequestBehavior.AllowGet);
        }


    }
}