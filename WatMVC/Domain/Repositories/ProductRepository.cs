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
    public class ProductRepository
    {
        public Product GetGoodsByID(int link_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Goods_Get");
            db.AddParameter(new SqlParameter("@link_id", link_id));
            db.AddParameter(new SqlParameter("@userSale", 0));
            return db.Query<Product>()[0];
        }

    }
}
