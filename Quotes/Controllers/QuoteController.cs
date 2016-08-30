using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quotes.DAL;

namespace Quotes.Controllers
{
    public class QuoteController : Controller
    {
        // GET: Quote
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyQuotes()
        {

            return View(QuoteDAL.FindUserQuotes(HttpContext.User.Identity.Name));
        }
    }
}