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
        // GET: /catalog/vyklyuchateli/
        [Route("catalog/{subject_name}")]
        public ActionResult Subject(string subject_name)
        {
            var cs = new CatalogService();
            var catalogGoods = cs.GetGoodsBySubject(subject_name);

            return View("Subject", new CatalogViewModels() { CatalogGoods = catalogGoods });
        }

        // GET: /catalog/vyklyuchateli/adidas
        [Route("catalog/{subject_name}/{brand_name}")]
        public ActionResult BrandSubject(string subject_name, string brand_name)
        {
            var cs = new CatalogService();
            var catalogGoods = cs.GetGoodsByBrandSubject(subject_name, brand_name);

            return View("BrandSubject", new CatalogViewModels() { CatalogGoods = catalogGoods });
        }

        // GET: /brand/adidas/
        [Route("brand/{brand_name}")]
        public ActionResult Brand(string brand_name)
        {
            var cs = new CatalogService();
            var catalogGoods = cs.GetGoodsByBrand(brand_name);

            return View("Brand", new CatalogViewModels() { CatalogGoods = catalogGoods });
        }

	}
}