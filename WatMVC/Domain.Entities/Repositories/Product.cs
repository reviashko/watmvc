using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class Product
    {
        public int Link_id { get; set; }

        public decimal Weight { get; set; }

        public byte Sale { get; set; }

        public string Brand_name { get; set; }

        public string Name { get; set; }

        public string Descr { get; set; }

        public string Descr_Seo { get; set; }

        public string Descr_SeoSrc { get; set; }

        public byte Sound { get; set; }

        public string UT_video { get; set; }

        public string Info { get; set; }

        public string Url { get; set; }

        public string Consists_name { get; set; }

        public string Country_name { get; set; }

        public int Logos_id { get; set; }

        public string Logos_name { get; set; }

        public int Gtype_id { get; set; }

        public string Gtype_name { get; set; }

        public string Seria_name { get; set; }

        public bool HasImg { get; set; }

        public string Color_name { get; set; }

        public string Color_web { get; set; }

        public string Articul { get; set; }

        public int Price { get; set; }

        public int OldPrice { get; set; }

        public int Group_id { get; set; }

        public bool HasExInfo { get; set; }

        public int RootGroup_id { get; set; }

        public string Brand_url { get; set; }

        public string Seria_url { get; set; }

        public byte Pdf { get; set; }

        public DataTable Recommendation { get; set; }

        public Product()
        {
            Weight = 0;
            Sale = 0;
            Brand_name = "";
            Name = "";
            Descr = "";
            Descr_Seo = "";
            Descr_SeoSrc = "";
            Sound = 0;
            UT_video = "";
            Url = "";
            Info = "";
            Consists_name = "";
            Country_name = "";
            Gtype_name = "";
            Seria_name = "";
            HasImg = false;
            Color_name = "";
            Color_web = "";
            Articul = "";
            Price = 0;
            OldPrice = 0;
            Group_id = 0;
            HasExInfo = false;
            RootGroup_id = 0;
            Brand_url = "";
            Seria_url = "";
            Pdf = 0;
        }
    }
}