using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace Domain.Entities
{
    public class dev_xml_mechs
    {
        public int Link_id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Price_id { get; set; }

        public int GType_id { get; set; }

        public dev_xml_mechs()
        {
            Link_id = 0;
            Name = "";
            Price = 0;
            Price_id = 0;
            GType_id = 0;
        }
    }
}