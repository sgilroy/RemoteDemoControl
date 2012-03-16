using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using RemoteDemoControl.Models;

namespace RemoteDemoControl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = SystemStateModel.GetSystemState();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult StartDemo()
        {
            if (Request.HttpMethod == "POST")
            {
                DemoLaunchingClient client = new DemoLaunchingClient();
                ViewBag.Message = client.StartDemo();
            }
            return View("Index");
        }

        public ActionResult StopDemo()
        {
            if (Request.HttpMethod == "POST")
            {
                DemoLaunchingClient client = new DemoLaunchingClient();
                ViewBag.Message = client.StopDemo();
            }
            return View("Index");
        }

    }
}
