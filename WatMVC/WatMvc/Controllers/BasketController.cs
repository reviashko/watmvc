using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using WatMvc.Models;

namespace WatMvc.Controllers
{
    public class BasketController : Controller
    {
        IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [Route("basket/")]
        public ActionResult Index()
        {
            int client_id = 1;
            var basketItems = _basketService.Get(client_id);

            return View("Index", new BasketViewModels() { Products = basketItems });
        }

        [HttpPost]
        public JsonResult Get()
        {
            int client_id = 1;
            var basketItems = _basketService.Get(client_id);
            return Json(new { bit = basketItems }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int goods_id, byte cnt = 1)
        {
            int client_id = 1;
            return Json(new { name = String.Format("{0} {1}added" , goods_id, _basketService.Add(client_id, goods_id, cnt) ? "" : " not ") });
        }

        [HttpPost]
        public JsonResult Remove(int basket_id)
        {
            int client_id = 1;
            return Json(new { name = String.Format("{0}removed", _basketService.Remove(client_id, basket_id) ? "" : " not ") });
        }

    }
}