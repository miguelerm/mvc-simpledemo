using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        [Authorize]
        public ActionResult AuthenticatedDemo()
        {
            ViewBag.Message = "Page, everyone authenticated access";
            return View();
        }

        [Authorize(Roles = "Muggles")]
        public ActionResult UsersDemo()
        {
            ViewBag.Message = "Page, users only access";
            return View();
        }

        [Authorize(Roles = "Almighties")]
        public ActionResult AdminsDemo()
        {
            ViewBag.Message = "Page, Admins only access";
            return View();
        }

        [Authorize(Roles = "Almighties, Muggles")]
        public ActionResult AdminsUsersDemo()
        {
            ViewBag.Message = "Page, users or admins access";
            return View();
        }

    }
}