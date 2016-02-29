using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Domain
{
    public class MenuRepository : IMenuRepository
    {
        public List<MenuItem> GetMenuItems()
        {
            List<MenuItem> retval = new List<MenuItem>();

            retval.Add(new MenuItem { Item_name = "home", Item_url = "/" });
            retval.Add(new MenuItem { Item_name = "basket", Item_url = "/basket" });
            retval.Add(new MenuItem { Item_name = "Zamel Sundi", Item_url = "/catalog/all/zamel/sundi/" });
            retval.Add(new MenuItem { Item_name = "Merten Antik", Item_url = "/catalog/all/merten/antik/" });

            return retval;

            /*
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Goods_Get");
            db.AddParameter(new SqlParameter("@link_id", link_id));
            db.AddParameter(new SqlParameter("@userSale", 0));
            return db.Query<Product>()[0];
            */
        }

    }
}
