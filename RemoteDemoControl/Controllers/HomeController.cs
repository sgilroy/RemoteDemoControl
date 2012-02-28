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
            ViewBag.Message = "Demo state unknown.";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult StartDemo()
        {
            ViewBag.Message = "Starting demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            client.StartDemo();
            return View("Index");
        }

        public ActionResult StopDemo()
        {
            ViewBag.Message = "Stopping demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            client.StopDemo();
            return View("Index");
        }

        public ActionResult FloorFlock()
        {
            ViewBag.Message = "Starting Floor Flock demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            client.LaunchRemoteProcess("Demos\\FloorFlock.ahk");
            return View("Index");
        }

        public ActionResult AtriumFloorPlay()
        {
            ViewBag.Message = "Starting Atrium Floor Play demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            client.LaunchRemoteProcess("Demos\\AtriumFloorPlay.ahk");
            return View("Index");
        }
    }
}
