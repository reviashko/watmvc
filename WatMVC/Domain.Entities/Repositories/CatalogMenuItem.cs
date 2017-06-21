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
        public int Item_id { get; set; }

        public int Brand_id { get; set; }

        public string Brand_name { get; set; }

        public int Seria_id { get; set; }

        public string Seria_name { get; set; }

        public int Category_id { get; set; }

        public string Category_name { get; set; }        

        public string Item_url
        {
            get { return String.Format("/catalog/{0}/{1}/{2}/", Category_name, Brand_name, Seria_name); }
        }

        public string Item_name
        {
            get { return String.Format("{0} {1}", Brand_name.Substring(0, 1).ToUpper() +  Brand_name.Substring(1), Seria_name.Substring(0, 1).ToUpper() + Seria_name.Substring(1)); }
        }

        public CatalogMenuItem()
        {
            Item_id = 0;
            Brand_id = 0;
            Brand_name = "";
            Seria_id = 0;
            Seria_name = "";
            Category_id = 0;
            Category_name = "";
        }

    }
}