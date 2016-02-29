using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using Domain.Entities;

namespace Domain
{
    public class BasketRepository : IBasketRepository
    {
        public bool Add(int client_id, int goods_id, byte count)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Basket_Add");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            db.AddParameter(new SqlParameter("@goods_id", goods_id));
            db.AddParameter(new SqlParameter("@cnt", count));

            return db.GetReturnValue<bool>();
        }

        public List<Product> Get(int client_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Basket_Get");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            return db.Query<Product>();
        }
    }
}