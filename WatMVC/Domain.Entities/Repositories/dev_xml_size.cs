using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace Domain.Entities
{
    public class dev_xml_size
    {

        public int Id { get; set; }

        public decimal Price { get; set; }

        public string Size_name { get; set; }

        public int Quantity { get; set; }

        public int Id_1s { get; set; }

        public dev_xml_size()
        {
            Id = 0;
            Price = 0;
            Size_name = "";
            Quantity = 0;
            Id_1s = 0;
        }
    }
}