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
        public bool Add(int client_id, int articul, byte count)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Basket_Add");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            db.AddParameter(new SqlParameter("@articul", articul));
            db.AddParameter(new SqlParameter("@cnt", count));

            return db.GetReturnValue<bool>();
        }

        public int SaveOrder(int client_id, string pay_type)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Order_Save");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            db.AddParameter(new SqlParameter("@pay_type", pay_type));

            return db.GetReturnValue<int>();
        }

        public bool Remove(int client_id, int articul)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Basket_Remove");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            db.AddParameter(new SqlParameter("@articul", articul));

            return db.GetReturnValue<bool>();
        }

        public List<BasketItem> Get(int client_id)
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.Basket_Get");
            db.AddParameter(new SqlParameter("@client_id", client_id));
            return db.Query<BasketItem>();
        }
    }
}