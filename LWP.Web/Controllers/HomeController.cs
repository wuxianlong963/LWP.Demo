using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LWP.Lib;

namespace LWP.Web.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            var productClass = new ProductClass()
            {
                Name = "test name 001",
                ParentId = 0
            };

            (new ProductClassManager()).AddProductClass(productClass);

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
