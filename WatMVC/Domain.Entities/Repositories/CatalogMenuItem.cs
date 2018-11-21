using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class CatalogMenuItem
    {
        public int Menu_id { get; set; }

        public int Menu_pid { get; set; }

        public string Menu_name { get; set; }
        public string Menu_url { get; set; }

        public CatalogMenuItem()
        {
            Menu_id = 0;
            Menu_pid = 0;
            Menu_name = "";
            Menu_url = "";
        }

    }
}