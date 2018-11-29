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
    public class CatalogRepository : ICatalogRepository
    {
        public List<int> GetArticulsByMenuId(int menu_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Catalogs_GetItems");
            db.AddParameter(new SqlParameter("@menu_id", menu_id));

            return db.Query<int>();
        }

        public List<Product> GetArticulsByArticuls(byte[] articuls_bin)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Products_Get");
            db.AddParameter(new SqlParameter("@articuls_bin", articuls_bin));

            return db.Query<Product>();
        }

    }
}
