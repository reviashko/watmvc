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
            if (menuItem.Menu_id < 1)
            {
                return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
            }

            var catalogGoods = _catalogService. GetGoodsByMenuId(menuItem.Menu_id);
            return Json(new { result = 1, itms = catalogGoods, mnus = menuItem }, JsonRequestBehavior.AllowGet);

        }

    }
}