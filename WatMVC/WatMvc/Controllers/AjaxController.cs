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
        IPagerService _pagerService;

        public AjaxController(ICatalogService catalogService, IMenuService menuService, IPagerService pagerService)
        {
            _catalogService = catalogService;
            _menuService = menuService;
            _pagerService = pagerService;
        }

        public JsonResult GetCatalogDataById(int menu_id, int page_id)
        {
            var menuItem = _menuService.GetCatalogMenuItemById(menu_id);
            if (menuItem.Menu_id < 1)
            {
                return Json(new { result = 0 }, JsonRequestBehavior.AllowGet);
            }

            var catalogGoods = _catalogService. GetGoodsByMenuId(menuItem.Menu_id, page_id, _pagerService.GetPageSize());
            var pagerData = _pagerService.GetPagerItems(_catalogService.GetGoodsByMenuIdCount(), _pagerService.GetPageSize(), page_id, _pagerService.GetPagerSize());

            return Json(new { result = 1, itms = catalogGoods, mnus = menuItem, pager = pagerData }, JsonRequestBehavior.AllowGet);

        }

    }
}