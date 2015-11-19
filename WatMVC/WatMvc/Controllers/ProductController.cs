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

namespace WatMvc.Controllers
{
    public class ProductController : Controller
    {

        [Route("product/{link_id}")]
        public ActionResult Index(int link_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("WebSite.Goods_Get");
            db.AddParameter(new SqlParameter("@link_id", link_id));
            db.AddParameter(new SqlParameter("@userSale", 0));
            DataSet dataSet = db.GetDataSet();
            DataTable src = dataSet.Tables[0];

            dev_xgoods xnGoods = new dev_xgoods(link_id, src);

            ViewBag.Message = string.Format("Your article is : {0}", xnGoods.Brand_name);

            return View();
        }

	}
}