using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace Domain.Entities
{
    public class dev_xml_subgoods
    {

        public int Id { get; set; }

        public int Color_id { get; set; }

        public string Color_name { get; set; }

        public string Articul { get; set; }

        public string Color_web { get; set; }

        public List<dev_xml_size> Sizes { get; set; }

        public dev_xml_subgoods()
        {
            Id = 0;
            Color_id = 0;
            Color_name = "";
            Articul = "";
            Color_web = "";
            Sizes = new List<dev_xml_size>();
        }
    }
}