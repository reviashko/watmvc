using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application;
using WatMvc.Models;

namespace WatMvc.Views
{
    public class CatalogController : Controller
    {
        [Route("catalog/{brand_name}/{seria_name}/{articul}")]
        public ActionResult BrandSeria(string brand_name, string seria_name, string articul)
        {
            var cs = new CatalogService();
            var catalogGoods = cs.GetGoodsByBrandSeria(brand_name, seria_name);

            return View("Product", new CatalogViewModels() { CatalogGoods = catalogGoods });
        }

	}
}