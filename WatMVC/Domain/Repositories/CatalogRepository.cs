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
        public List<Product> GetGoodsByCategoryBrandSeriaArticul(int category_id, int brand_id, int seria_id, string articul)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@category_id", category_id));
            db.AddParameter(new SqlParameter("@brand_id", brand_id));
            db.AddParameter(new SqlParameter("@seria_id", seria_id));
            db.AddParameter(new SqlParameter("@articul", articul));

            return db.Query<Product>();
        }

        public List<Product> GetGoodsByCategoryBrandSeria(int category_id, int brand_id, int seria_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@category_id", category_id));
            db.AddParameter(new SqlParameter("@brand_id", brand_id));
            db.AddParameter(new SqlParameter("@seria_id", seria_id));

            return db.Query<Product>();
        }

        public List<Product> GetGoodsByCategoryBrand(int category_id, int brand_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@category_id", category_id));
            db.AddParameter(new SqlParameter("@brand_id", brand_id));

            return db.Query<Product>();
        }

        public List<Product> GetGoodsByCategory(int category_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.GoodsFilter");
            db.AddParameter(new SqlParameter("@category_id", category_id));

            return db.Query<Product>();
        }


    }
}
