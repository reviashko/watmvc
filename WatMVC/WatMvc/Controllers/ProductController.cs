using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Mvc;
using Domain;
using System.Configuration;
using System.Data;
using Domain.Entities;
using Application;
using WatMvc.Models;


namespace WatMvc.Controllers
{
    public class ProductController : Controller
    {

        [Route("product/{link_id}")]
        public ActionResult Index(int link_id)
        {
            ProductService pm = new ProductService();
            Product xnGoods = pm.GetGoodsByID(link_id);

            return View(new ProductViewModels() {Product = xnGoods });
        }

	}
}