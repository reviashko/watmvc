using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

namespace Domain
{
    public class GoodsFiller
    {

        private IDataBase cdb;
        private IXMLReader xreader;
        public dev_xml_goods XGoods;

        public GoodsFiller(int link_id, IDataBase cdb, IXMLReader xreader, decimal userSale)
        {
            this.XGoods = new dev_xml_goods();

            this.cdb = cdb;
            this.cdb.SetStoredProcedure("WebSite.GetGoodsXML");
            this.cdb.AddParameter(new SqlParameter("@Link_id", link_id));
            this.cdb.AddParameter(new SqlParameter("@UserSale", userSale));

            this.xreader = xreader;
            this.xreader.LoadXML(this.cdb.GetScalarValue());

            XGoods.Brand_id = this.xreader.GetAttribute<int>("brand_id", 0);
            XGoods.Link_id = this.xreader.GetAttribute<int>("link_id", 0);
            XGoods.Group_id = this.xreader.GetAttribute<int>("group_id", 0);
            XGoods.Root = this.xreader.GetAttribute<int>("root", 0);
            XGoods.Sound = this.xreader.GetAttribute<int>("sound", 0);
            XGoods.Info = this.xreader.GetAttribute<int>("info", 0);
            XGoods.Consists_id = this.xreader.GetAttribute<int>("consists_id", 0);
            XGoods.Country_id = this.xreader.GetAttribute<int>("country_id", 0);
            XGoods.Gtype_id = this.xreader.GetAttribute<int>("gtype_id", 0);
            XGoods.Seria_id = this.xreader.GetAttribute<int>("seria_id", 0);
            XGoods.Weight = this.xreader.GetAttribute<decimal>("weight", 0);
            XGoods.Sale = this.xreader.GetAttribute<byte>("sale", 0);
            XGoods.Seria_name = this.xreader.GetAttribute<string>("seria_name", "");
            XGoods.Consists_name = this.xreader.GetAttribute<string>("consists_name", "");
            XGoods.Gtype_name = this.xreader.GetAttribute<string>("gtype_name", "");
            XGoods.Country_name = this.xreader.GetAttribute<string>("country_name", "");
            XGoods.UT_video = this.xreader.GetAttribute<string>("ut_video", "");
            XGoods.Name = this.xreader.GetAttribute<string>("name", "");
            XGoods.Brand_name = this.xreader.GetAttribute<string>("brand_name", "");
            XGoods.HasImg = this.xreader.GetAttribute<string>("has_img", "0").Equals("1") ? true : false;
            XGoods.Descr = this.xreader.GetValue<string>("//link/descr", "").Replace(@"\n", "<br/>");
            XGoods.Descr_Seo = this.xreader.GetValue<string>("//link/descr_seo", "");

            //filling goods properies
            XmlNodeList goodsList = this.xreader.GetXmlNodes("//link//goods//good");
            foreach (XmlNode child in goodsList)
            {
                SGoodsFiller sgFiller = new SGoodsFiller(child, this.xreader);
                XGoods.Items.Add(sgFiller.XSGoods);
            }

            //filling goods mechs
            XmlNodeList mechList = this.xreader.GetXmlNodes("//link//mechs//mech");
            foreach (XmlNode child in mechList)
            {
                GoodsMechsFiller smFiller = new GoodsMechsFiller(child, this.xreader);
                XGoods.Mechs.Add(smFiller.Mech);
            }

            //filling goods sames
            XmlNodeList sameList = this.xreader.GetXmlNodes("//link//sames//same");
            foreach (XmlNode child in sameList)
            {
                GoodsSamesFiller gsameFiller = new GoodsSamesFiller(child, this.xreader);
                XGoods.Sames.Add(gsameFiller.Same);
            }
        }
    }


}