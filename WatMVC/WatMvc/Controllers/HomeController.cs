using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using WatMvc.Models;

namespace WatMvc.Controllers
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

        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var ms = new MenuService();
            var menuItems = ms.GetMenuItems();

            return PartialView("MainMenuPartial", new MainMenuViewModels() { MenuItems = menuItems });
        }

        public ActionResult TestDetails()
        {
            return PartialView("TestPartial");
        }
    }
}