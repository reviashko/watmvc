using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Domain
{
    public class dev_order_mng
    {
        IDataBase db;

        public dev_order_mng(IDataBase db)
        {
            this.db = db;
        }

        public DataTable GetExOrderGoods(int order_id, int user_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Order_id", order_id));
            db.AddParameter(new SqlParameter("@User_id", user_id));
            db.SetStoredProcedure("PersonalCabinet.GetExOrderGoods");
            return db.GetDataTable();
        }

        public DataTable SendQiwiOrder(int order_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Order_id", order_id));
            db.SetStoredProcedure("PersonalCabinet.SendQiwiOrder");
            return db.GetDataTable();
        }

        public DataTable GetOrderList(int status_id)
        {
            db.ClearParams();
            if (status_id > 0)
            {
                db.AddParameter(new SqlParameter("@Status_id", status_id));
            }
            db.SetStoredProcedure("Orders.GetAdminOrderList");
            return db.GetDataTable();
        }

        public int DeleteUserOrder(int order_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Order_id", order_id));
            db.SetStoredProcedure("Orders.DeleteOrderById");
            return db.GetDataTable().Rows.Count;
        }

        public int UpdateOrderInfo(int order_id, int status_id, DateTime shiping_date, string comment, int schet_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@Order_id", order_id));
            db.AddParameter(new SqlParameter("@Status_id", status_id));
            db.AddParameter(new SqlParameter("@Shiping_date", shiping_date));
            db.AddParameter(new SqlParameter("@Comment", comment));
            db.AddParameter(new SqlParameter("@Schet_id", schet_id));
            db.SetStoredProcedure("Orders.UpdateOrderInfo");
            return db.GetDataTable().Rows.Count;
        }

        public int SaveUserOrder(int user_id, int oplata_id, int dostavka_id)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@User_id", user_id));
            db.AddParameter(new SqlParameter("@Oplata_id", oplata_id));
            db.AddParameter(new SqlParameter("@Dostavka_id", dostavka_id));
            db.SetStoredProcedure("Orders.Add");
            return int.Parse(db.GetScalarValue());
        }

        public int SaveFastUserOrder(string phone, int link_id, int quantity)
        {
            db.ClearParams();
            db.AddParameter(new SqlParameter("@link_id", link_id));
            db.AddParameter(new SqlParameter("@quantity", quantity));
            db.AddParameter(new SqlParameter("@phone", phone));
            db.SetStoredProcedure("Orders.AddFastOrder");
            return int.Parse(db.GetScalarValue());
        }

    }
}