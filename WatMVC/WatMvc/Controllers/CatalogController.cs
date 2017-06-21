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
            var catalogGoods = _catalogService.GetGoodsByCategoryBrandSeria(menuItem.Category_id, menuItem.Brand_id, menuItem.Seria_id);
            return Json(new { itms = catalogGoods, mnus = menuItem }, JsonRequestBehavior.AllowGet);
        }
        
        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var menuItems = _menuService.GetMainMenuItems();
            return PartialView("MainMenuPartial", new MainMenuViewModels() { MenuItems = menuItems });
        }
        

        [Route("catalog/{category_name}/{brand_name}/{seria_name}/{articul}")]
        public ActionResult SeriaProduct(string category_name, string brand_name, string seria_name, string articul)
        {
            var menuItem = _menuService.GetCatalogMenuItemByCategoryBrandSeria(category_name.ToLower(), brand_name.ToLower(), seria_name.ToLower());
            if (menuItem == null)
            {
                return HttpNotFound("Ресурс не найден");
            }

            var catalogGoods = _catalogService.GetGoodsByCategoryBrandSeriaArticul(menuItem.Category_id, menuItem.Brand_id, menuItem.Seria_id, articul);
            if(catalogGoods.Count > 0)
            {
                var catalogMenu = _menuService.GetCatalogMenuItems(menuItem.Category_id);
                return View("Product", new ProductViewModels() { Product = catalogGoods[0], MenuItems = catalogMenu });
            }
            else
            {
                return HttpNotFound("Ресурс не найден");
            }
            
        }

        [Route("catalog/{category_name}/{brand_name}/{seria_name}")]
        public ActionResult CategoryBrandSeria(string category_name, string brand_name, string seria_name)
        {
            var menuItem = _menuService.GetCatalogMenuItemByCategoryBrandSeria(category_name.ToLower(), brand_name.ToLower(), seria_name.ToLower());
            if (menuItem == null)
            {
                return HttpNotFound("Ресурс не найден");
            }

            var catalogGoods = _catalogService.GetGoodsByCategoryBrandSeria(menuItem.Category_id, menuItem.Brand_id, menuItem.Seria_id);
            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("Ресурс не найден");
            }
            var catalogMenu = _menuService.GetCatalogMenuItems(menuItem.Category_id);
            //ViewBag.Title = "Ресурс не найден";
            return View("Catalog", new CatalogViewModels() { Products = catalogGoods, MenuItems = catalogMenu });
        }

        [Route("catalog/{category_name}/{brand_name}")]
        public ActionResult CategoryBrand(string category_name, string brand_name)
        {
            var menuItem = _menuService.GetCatalogMenuItemByCategoryBrand(category_name.ToLower(), brand_name.ToLower());
            if (menuItem == null)
            {
                return HttpNotFound("Ресурс не найден");
            }

            var catalogGoods = _catalogService. GetGoodsByCategoryBrand(menuItem.Category_id, menuItem.Brand_id);
            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("Ресурс не найден");
            }

            //ViewBag.Title = "Ресурс не найден";
            var catalogMenu = _menuService.GetCatalogMenuItems(menuItem.Category_id);

            return View("Catalog", new CatalogViewModels() { Products = catalogGoods, MenuItems = catalogMenu });
        }

        [Route("catalog/{category_name}")]
        public ActionResult Category(string category_name)
        {
            var menuItem = _menuService.GetCatalogMenuItemByCategory(category_name.ToLower());
            if (menuItem == null)
            {
                return HttpNotFound("Ресурс не найден");
            }

            var catalogGoods = _catalogService.GetGoodsByCategory(menuItem.Category_id);
            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("Ресурс не найден");
            }
            var catalogMenu = _menuService.GetCatalogMenuItems(menuItem.Category_id);

            return View("Catalog", new CatalogViewModels() { Products = catalogGoods, MenuItems = catalogMenu });
        }

    }
}