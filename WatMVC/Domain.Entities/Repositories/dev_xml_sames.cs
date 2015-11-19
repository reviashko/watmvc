using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace Domain.Entities
{
    public class dev_xml_sames
    {
        public int Link_id { get; set; }

        public string Color_name { get; set; }

        public string Color_web { get; set; }

        public dev_xml_sames()
        {
            Link_id = 0;
            Color_name = "";
            Color_web = "";
        }
    }
}