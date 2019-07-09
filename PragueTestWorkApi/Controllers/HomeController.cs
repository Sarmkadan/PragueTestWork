using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PragueTestWorkApi.Models;

namespace PragueTestWorkApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult View()
        {
            UrlLog.UrlLogdbContext contexdb = new UrlLog.UrlLogdbContext();
        
            return View(contexdb.ItemDbSet);
        }
    }
}
