using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using Domain.Entities;
using WatMvc.Models;
using System.IO;

namespace WatMvc.Views
{
    public class CatalogController : Controller
    {
        ICatalogService _catalogService;
        IMenuService _menuService;

        public CatalogController(ICatalogService catalogService, IMenuService menuService)
        {
            _catalogService = catalogService;
            _menuService = menuService;
        }

        public JsonResult GetCatalogDataById(int menu_id)
        {
            var menuItem = _menuService.GetCatalogMenuItemById(menu_id);
            var catalogGoods = _catalogService.GetGoodsByBrandSeria(menuItem.Brand_id, menuItem.Seria_id);
            return Json(new { itms = catalogGoods, mnus = menuItem }, JsonRequestBehavior.AllowGet);
        }

        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var menuItems = _menuService.GetMainMenuItems();
            return PartialView("MainMenuPartial", new MainMenuViewModels() { MenuItems = menuItems });
        }

        [ChildActionOnly]
        public PartialViewResult CatalogMenu()
        {
            var menuItems = _menuService.GetCatalogMenuItems();
            return PartialView("CatalogMenuPartial", new CatalogMenuViewModels() { MenuItems = menuItems });
        }

        [Route("catalog/{brand_name}/{seria_name}/{articul}")]
        public ActionResult SeriaProduct(string brand_name, string seria_name, string articul)
        {
            var menuItem = _menuService.GetCatalogMenuItemByAttr(brand_name, seria_name);
            var catalogGoods = _catalogService.GetGoodsByBrandSeriaArticul(menuItem.Brand_id, menuItem.Seria_id, articul);

            if(catalogGoods.Count > 0)
            {
                return View("Product", new ProductViewModels() { Product = catalogGoods[0] });
            }
            else
            {
                return HttpNotFound("Ресурс не найден");
            }
            
        }

        [Route("catalog/{brand_name}/{seria_name}")]
        public ActionResult SeriaProducts(string brand_name, string seria_name)
        {
            var menuItem = _menuService.GetCatalogMenuItemByAttr(brand_name, seria_name);
            var catalogGoods = _catalogService.GetGoodsByBrandSeria(menuItem.Brand_id, menuItem.Seria_id);

            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("Ресурс не найден");
            }

            ViewBag.Title = "Ресурс не найден";

            return View("Catalog", new CatalogViewModels() { Products = catalogGoods });
        }

    }
}