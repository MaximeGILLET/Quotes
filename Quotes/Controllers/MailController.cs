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
        public ActionResult Index(bool? sent = null, bool? archived = null)
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

        public PartialViewResult List(bool? sent = null,bool? archived = null)
        {
            return PartialView("_Detail", DAL.MailDAL.UserMailList(User.Identity.GetUserId<int>()));
        }

        [HttpPost]
        public JsonResult Send(MailModel model)
        {

            try
            {
                DAL.MailDAL.SaveMail(model);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = e });
            }
            return Json(new { success = true, message = "Mail Sent successfully." }, JsonRequestBehavior.AllowGet);

        }

    }
}