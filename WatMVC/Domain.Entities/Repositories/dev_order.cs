using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Domain.Entities
{
    public class dev_order
    {
        public int Id { get; set; }

        public int Status_id { get; set; }

        public DateTime Ord_date { get; set; }

        public int User_id { get; set; }

        public int Oplata_id { get; set; }

        public int Dostavka_id { get; set; }


        public dev_order()
        {
            Id = 0;
            Status_id = 0;
            Ord_date = DateTime.Now;
            User_id = 0;
            Oplata_id = 0;
            Dostavka_id = 0;
        }

        public dev_order(DataRow row)
        {
            Id = Convert.ToInt32(row["id"].ToString());
            Status_id = Convert.ToInt32(row["status_id"].ToString());
            Ord_date = Convert.ToDateTime(row["ord_date"].ToString());
            User_id = Convert.ToInt32(row["user_id"].ToString());
            Oplata_id = Convert.ToInt32(row["oplata_id"].ToString());
            Dostavka_id = Convert.ToInt32(row["dostavka_id"].ToString());
        }

    }
}