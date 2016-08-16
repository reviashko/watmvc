using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class MainMenuItem
    {
        public int Item_id { get; set; }

        private string _item_name;
        public string Item_name{ get; set; }

        public string Item_url { get; set; }

        public MainMenuItem()
        {
            Item_url = "";
            Item_name = "";
            Item_id = 0;
        }

    }
}