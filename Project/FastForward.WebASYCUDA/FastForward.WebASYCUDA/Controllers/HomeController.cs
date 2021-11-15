using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FastForward.WebASYCUDA.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            string userId = HttpContext.Session["UserID"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
                return View();
            }
            else {
                return Redirect("~/Login/Login");
            }
        }

        public ActionResult About()
        {
            string userId = HttpContext.Session["UserID"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.Message = "Your application description page.";
                return View();
            }
            else
            {
                return Redirect("~/Login/Login");
            }
        }

        public ActionResult Contact()
        {
            string userId = HttpContext.Session["UserID"] as string;
            if (!string.IsNullOrEmpty(userId))
            {
                ViewBag.Message = "Your contact page.";
                return View();
            }
            else
            {
                return Redirect("~/Login/Login");
            }
        }
    }
}