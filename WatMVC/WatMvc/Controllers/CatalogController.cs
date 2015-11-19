using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatMvc.Views
{
    public class CatalogController : Controller
    {
        //
        // GET: /Catalog/e-333-5f/
        [Route("catalog/{articul}")]
        public ActionResult Index(string articul)
        {
            ViewBag.Message = string.Format("Your article is : {0}", articul);
            return View();
        }

	}
}