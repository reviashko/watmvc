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
    public class CatalogRepository
    {

        public List<Product> GetGoodsBySubject(string subject_name)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@subject_name", subject_name));
            return db.Query<Product>();
        }

        public List<Product> GetGoodsByBrandSubject(string subject_name, string brand_name)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@subject_name", subject_name));
            db.AddParameter(new SqlParameter("@brand_name", brand_name));
            return db.Query<Product>();
        }

        public List<Product> GetGoodsByBrand(string brand_name)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@brand_name", brand_name));
            return db.Query<Product>();
        }

    }
}
