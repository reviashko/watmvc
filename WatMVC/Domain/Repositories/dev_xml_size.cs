using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Domain.Entities;

namespace Domain
{

    public class SGoodsSizeFiller
    {
        private IXMLReader xreader;
        public dev_xml_size XSize { get; set; }

        public SGoodsSizeFiller(XmlNode node, IXMLReader xreader)
        {
            this.xreader = xreader;
            XSize = new dev_xml_size();
            FillSGoodsSizes(node);
        }

        private void FillSGoodsSizes(XmlNode node)
        {
            XSize.Price = this.xreader.GetNodeAttribute<decimal>(node, "price", 0);
            XSize.Size_name = this.xreader.GetNodeAttribute<string>(node, "size_name", "");
            XSize.Quantity = this.xreader.GetNodeAttribute<int>(node, "quantity", 0);
            XSize.Id_1s = this.xreader.GetNodeAttribute<int>(node, "id_1s", 0);
            XSize.Id = this.xreader.GetNodeAttribute<int>(node, "id", 0);
        }
    }
}