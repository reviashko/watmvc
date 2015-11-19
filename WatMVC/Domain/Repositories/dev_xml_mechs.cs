using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Domain.Entities;

namespace Domain
{

    public class GoodsMechsFiller
    {
        private IXMLReader xreader;
        public dev_xml_mechs Mech { get; set; }

        public GoodsMechsFiller(XmlNode node, IXMLReader xreader)
        {
            this.xreader = xreader;
            Mech = new dev_xml_mechs();
            FillGoodsMechs(node);
        }

        private void FillGoodsMechs(XmlNode node)
        {
            Mech.Name = this.xreader.GetNodeAttribute<string>(node, "name", "");
            Mech.Price = this.xreader.GetNodeAttribute<decimal>(node, "price", 0);
            Mech.Price_id = this.xreader.GetNodeAttribute<int>(node, "price_id", 0);
            Mech.Link_id = this.xreader.GetNodeAttribute<int>(node, "link_id", 0);
            Mech.GType_id = this.xreader.GetNodeAttribute<int>(node, "gtype_id", 0);
        }
    }
}