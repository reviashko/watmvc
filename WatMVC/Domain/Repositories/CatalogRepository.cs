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
        public List<Product> GetGoodsByMenuId(int menu_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Catalogs_GetItems");
            db.AddParameter(new SqlParameter("@menu_id", menu_id));

            return db.Query<Product>();
        }


    }
}
