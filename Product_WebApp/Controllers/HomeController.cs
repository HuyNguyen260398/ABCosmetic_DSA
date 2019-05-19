using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Product_WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Staff()
        {
            return View();
        }
        public ActionResult Manager()
        {
            return View();
        }
    }
}