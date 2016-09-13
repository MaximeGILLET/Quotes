using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Quotes.Models;

namespace Quotes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(bool? shouldAuth,string returnUrl)
        {
            
            ViewData["OpenAuthorizationPopup"] = shouldAuth;
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Announcements()
        {

            return PartialView("_Announcements",new List<AnnouncementModel>());
        }
    }
}