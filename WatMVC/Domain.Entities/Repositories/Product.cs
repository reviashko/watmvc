using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public int Sound { get; set; }

        public string UT_video { get; set; }

        public string Info { get; set; }

        public string Url { get; set; }

        public string Consists_name { get; set; }

        public string Country_name { get; set; }

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


        public Product(int link_id, DataTable src)
        {
            Link_id = link_id;
            Weight = decimal.Parse(src.Rows[0]["weight"].ToString());
            Sale = byte.Parse(src.Rows[0]["sale"].ToString());
            Brand_name = src.Rows[0]["brand_name"].ToString();
            Name = src.Rows[0]["name"].ToString();
            Descr = src.Rows[0]["descr"].ToString();
            Descr_SeoSrc = src.Rows[0]["descr_seo"].ToString();
            Descr_Seo = Descr_SeoSrc;
            Sound = int.Parse(src.Rows[0]["sound"].ToString());
            UT_video = src.Rows[0]["ut_video"].ToString();
            Consists_name = src.Rows[0]["consists_name"].ToString();
            Country_name = src.Rows[0]["country_name"].ToString();
            Gtype_name = src.Rows[0]["gtype_name"].ToString();
            Seria_name = src.Rows[0]["seria_name"].ToString();
            HasImg = src.Rows[0]["s_img"].ToString().ToUpper().Equals("TRUE");
            Color_name = src.Rows[0]["color_name"].ToString();
            Color_web = src.Rows[0]["color_web"].ToString();
            Articul = src.Rows[0]["articul"].ToString();
            Price = int.Parse(src.Rows[0]["price"].ToString());
            OldPrice = int.Parse(src.Rows[0]["price"].ToString());
            Group_id = int.Parse(src.Rows[0]["group_id"].ToString());
            HasExInfo = src.Rows[0]["hasExInfo"].ToString().ToUpper().Equals("TRUE");
            Info = src.Rows[0]["info"].ToString();
            RootGroup_id = int.Parse(src.Rows[0]["root_group_id"].ToString());
            Brand_url = src.Rows[0]["brand_url"].ToString();
            Seria_url = src.Rows[0]["seria_url"].ToString();
            Pdf = byte.Parse(src.Rows[0]["pdf"].ToString());
            Url = src.Rows[0]["url"].ToString();
        }
    }
}