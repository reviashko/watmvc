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
        
        [ChildActionOnly]
        public PartialViewResult MainMenu()
        {
            var menuItems = _menuService.GetMainMenuItems();
            return PartialView("MainMenuPartial", new MainMenuViewModels() { MenuItems = menuItems });
        }

        [ChildActionOnly]
        public PartialViewResult CatalogMenu()
        {
            var catalogMenu = _menuService.GetCatalogMenuItems(0);
            return PartialView("CatalogMenuPartial", new CatalogMenuViewModels() { MenuItems = catalogMenu });
        }

        [Route("catalog")]
        public ActionResult CategoryStart()
        {
            var catalogMenu = _menuService.GetCatalogMenuItems(0);

            var catalogGoods = _catalogService.GetGoodsByMenuId(0, 1, 9);

            return View("Catalog", new CatalogViewModels() { Products = catalogGoods, MenuItems = catalogMenu });
        }

        [Route("catalog/{brand_name}/{articul}/")]
        public ActionResult Category(string brand_name, int articul)
        {
            var catalogMenu = _menuService.GetCatalogMenuItems(0);

            var product = _catalogService.GetProductByArticul(articul, brand_name);
            if (product == null || product.Articul == 0)
            {
                return HttpNotFound("Адрес не найден");
            }

            return View("Product", new ProductViewModels() { Product = product, MenuItems = catalogMenu });
        }

        [Route("catalog/{menu_url}/")]
        public ActionResult Category(string menu_url)
        {
            var catalogMenuItem = _menuService.GetCatalogMenuItemByUrl(menu_url);
            if (catalogMenuItem == null)
            {
                return HttpNotFound("Адрес не найден");
            }

            var catalogMenu = _menuService.GetCatalogMenuItems(catalogMenuItem.Menu_id);

            var catalogGoods = _catalogService.GetGoodsByMenuId(catalogMenuItem.Menu_id, 1, 9);
            if (catalogGoods.Count < 1)
            {
                return HttpNotFound("Ресурс не найден");
            }

            return View("Catalog", new CatalogViewModels() { Products = catalogGoods, MenuItems = catalogMenu });
        }

    }
}