using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using WatMvc.Models;

namespace WatMvc.Views
{

    public class Employee
    {
        public string Employee_name { get; set; }
    }

    public class CatalogController : Controller
    {
        ICatalogService _catalogService;
        IMenuService _menuService;

        public CatalogController(ICatalogService catalogService, IMenuService menuService)
        {
            _catalogService = catalogService;
            _menuService = menuService;
        }

        public ActionResult JSONTest()
        {

            Employee vasya = new Employee
            {
                Employee_name = "Vasya"
            };

            return Json(new { employee = vasya }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var menuItems = _menuService.GetMenuItems();
            return PartialView("MainMenuPartial", new MainMenuViewModels() { MenuItems = menuItems });
        }

        [Route("catalog/{subject_name}/{brand_name}/{seria_name}/{articul}")]
        public ActionResult SeriaProduct(string subject_name, string brand_name, string seria_name, string articul)
        {
            var catalogGoods = _catalogService.GetGoodsByBrandSeriaArticul(subject_name, brand_name, seria_name, articul);

            return View("Product", new ProductViewModels() { Product = catalogGoods[0] });
        }

        [Route("catalog/{subject_name}/{brand_name}/{seria_name}")]
        public ActionResult SeriaProducts(string subject_name, string brand_name, string seria_name)
        {
            var catalogGoods = _catalogService.GetGoodsByBrandSeria(subject_name, brand_name, seria_name);

            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("уасся");
            }

            ViewBag.Title = "test title";

            return View("Catalog", new CatalogViewModels() { CatalogGoods = catalogGoods });
        }

    }
}