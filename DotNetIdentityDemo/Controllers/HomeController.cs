using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace DotNetIdentityDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = $"Your application description page. UserName:{User.Identity.GetUserName()} ID:{User.Identity.GetUserId()} IsTestRole:{User.IsInRole("Test")}";

            return View();
        }

        [Authorize(Roles = "Test")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
    }
}