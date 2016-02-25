using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WatMvc.Controllers
{
    public class ErrorsController : Controller
    {
        [Route("error/PageNotFound")]
        public ActionResult PageNotFound()
        {
            HttpContext.Response.StatusCode = 404;
            ViewBag.ReturnUrl = HttpContext.Request.Url.OriginalString;
            return View("PageNotFound");
        }

	}
}