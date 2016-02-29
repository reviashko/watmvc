using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class MenuItem
    {
        public string Item_name { get; set; }

        public string Item_url { get; set; }

        public MenuItem()
        {
            Item_name = "";
            Item_url = "";
        }

    }
}