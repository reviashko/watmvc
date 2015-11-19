using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Domain.Entities;

namespace Domain
{

    public class GoodsSamesFiller
    {
        private IXMLReader xreader;
        public dev_xml_sames Same { get; set; }

        public GoodsSamesFiller(XmlNode node, IXMLReader xreader)
        {
            this.xreader = xreader;
            Same = new dev_xml_sames();
            FillGoodsSames(node);
        }

        private void FillGoodsSames(XmlNode node)
        {
            Same.Link_id = this.xreader.GetNodeAttribute<int>(node, "link_id", 0);
            Same.Color_name = this.xreader.GetNodeAttribute<string>(node, "color_name", "");
            Same.Color_web = this.xreader.GetNodeAttribute<string>(node, "color_web", "");
        }
    }
}