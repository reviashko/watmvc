﻿using System;
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
        IPaymentService _paymentService;

        public BasketController(IBasketService basketService, IPaymentService paymentService)
        {
            _basketService = basketService;
            _paymentService = paymentService;
        }

        [Route("basket/")]
        public ActionResult Index()
        {
            int client_id = 1;
            var basketItems = _basketService.Get(client_id);
            var paymentTypes = _paymentService.Get();

            return View("Index", new BasketViewModels() { Products = basketItems, PaymentTypes = paymentTypes });
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