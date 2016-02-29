using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatMvc.Controllers
{
    public class BasketController : Controller
    {

        [HttpPost]
        public ActionResult Add(int goods_id)
        {
            //var result = _db.Ingredients.Where(i => i.IngredientName == input);
            return Json(new { name = String.Format("{0} Added", goods_id) });
        }

        // GET: Basket
        public ActionResult Index()
        {
            return View();
        }
    }
}