using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class PagerItem
    {
        public int Page_id { get; set; }

        public string Page_name { get; set; }

        public PagerItem()
        {
            Page_id = 0;
            Page_name = "";
        }

    }
}