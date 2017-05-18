using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quotes.Models
{
    [Authorize]
    public class MailController : Controller
    {
        // GET: Mail
        public ActionResult Index()
        {


            return View(DAL.MailDAL.UserMailList(User.Identity.GetUserId<int>()));
        }

        public PartialViewResult Create()
        {
            return PartialView("_Create");
        }

        public PartialViewResult Detail(int? id)
        {
            return PartialView("_Detail",new MailViewModel());
        }

        public PartialViewResult List()
        {
            return PartialView("_Detail", DAL.MailDAL.UserMailList(User.Identity.GetUserId<int>()));
        }

    }
}