using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace apiegitim1.Controllers
{
    // https://www.roketnot.com/not/1215-rest-web-api-mvc-ve-swagger
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
