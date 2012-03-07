using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RemoteDemoControl.Models;

namespace RemoteDemoControl.Controllers
{
    public class DemosController : Controller
    {
        //
        // GET: /Demos/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string button)
        {
            string demoName = button;
            //ViewBag.Message = "Starting " + demoName + " demo...";
            DemoLaunchingClient client = new DemoLaunchingClient();
            ViewBag.Message = client.LaunchRemoteProcess("Demos\\" + demoName);
            return View();
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
