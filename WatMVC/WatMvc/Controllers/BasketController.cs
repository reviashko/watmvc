using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using WatMvc.Models;
using Domain.Entities;

namespace WatMvc.Controllers
{
    public class BasketController : Controller
    {
        IBasketService _basketService;
        IPaymentService _paymentService;
        ICatalogService _catalogService;

        public BasketController(IBasketService basketService, IPaymentService paymentService, ICatalogService catalogService)
        {
            _basketService = basketService;
            _paymentService = paymentService;
            _catalogService = catalogService;
        }

        [Route("basket/")]
        public ActionResult Index()
        {
            int client_id = 1;
            var paymentTypes = _paymentService.Get();

            List<int> basketArticuls = _basketService.GetArticuls(client_id);
            List<Product> basketItems = _catalogService.GetProductsByArticuls(basketArticuls, 1, 100);

            return View("Index", new BasketViewModels() { Products = basketItems, PaymentTypes = paymentTypes });
        }

        [HttpPost]
        public JsonResult Get()
        {
             int client_id = 1;
            //var basketItems = _basketService.Get(client_id);

            List<int> basketArticuls = _basketService.GetArticuls(client_id);
            List<Product> basketItems = _catalogService.GetProductsByArticuls(basketArticuls, 1, 100);

            return Json(new { bit = basketItems }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(int articul, byte cnt = 1)
        {
            int client_id = 1;
            return Json(new { name = String.Format("{0} {1} added" , articul, _basketService.Add(client_id, articul, cnt) ? "" : " not ") });
        }

        [HttpPost]
        public JsonResult Remove(int articul)
        {
            int client_id = 1;
            return Json(new { name = String.Format("{0} removed", _basketService.Remove(client_id, articul) ? "" : " not ") });
        }

        [HttpPost]
        public JsonResult SaveOrder(int client_id, string pay_type)
        {
            if(!_paymentService.IsExists(pay_type))
            {
                return Json(new { name = "Нет такой оплаты" });
            }

            return Json(new { name = String.Format("order {0} saved", _basketService.SaveOrder(client_id, pay_type) ) });
        }

    }
}