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
        public List<Product> GetGoodsByBrandSeriaArticul(string subject_name, string brand_name, string seria_name, string articul)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@brand_name", brand_name));
            db.AddParameter(new SqlParameter("@seria_name", seria_name));
            db.AddParameter(new SqlParameter("@articul", articul));

            if (!subject_name.Equals("all"))
            {
                db.AddParameter(new SqlParameter("@subject_name", subject_name));
            }
            return db.Query<Product>();
        }

        public List<Product> GetGoodsByBrandSeria(string subject_name, string brand_name, string seria_name)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@brand_name", brand_name));
            db.AddParameter(new SqlParameter("@seria_name", seria_name));
            if (!subject_name.Equals("all"))
            {
                db.AddParameter(new SqlParameter("@subject_name", subject_name));
            }
            return db.Query<Product>();
        }
        

    }
}
