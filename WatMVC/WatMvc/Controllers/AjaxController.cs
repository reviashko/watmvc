using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;

namespace WatMvc.Controllers
{
    public class AjaxController : Controller
    {
        ICatalogService _catalogService;
        IMenuService _menuService;

        public AjaxController(ICatalogService catalogService, IMenuService menuService)
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

    }
}