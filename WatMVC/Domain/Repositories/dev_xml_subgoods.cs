using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using Domain.Entities;

namespace Domain
{

    public class SGoodsFiller
    {
        private IXMLReader xreader;

        public dev_xml_subgoods XSGoods { get; set; }

        public SGoodsFiller(XmlNode node, IXMLReader xreader)
        {
            this.xreader = xreader;
            XSGoods = new dev_xml_subgoods();
            FillSGoods(node);
        }

        private void FillSGoods(XmlNode node)
        {
            XSGoods.Id = this.xreader.GetNodeAttribute<int>(node, "id", 0);
            XSGoods.Color_id = this.xreader.GetNodeAttribute<int>(node, "color_id", 0);
            XSGoods.Color_name = this.xreader.GetNodeAttribute<string>(node, "color_name", "");
            XSGoods.Articul = this.xreader.GetNodeAttribute<string>(node, "articul", "");
            XSGoods.Color_web = this.xreader.GetNodeAttribute<string>(node, "color_web", "");

            foreach (XmlNode subNode in node.SelectNodes("//sizes/size"))
            {
                SGoodsSizeFiller gsFiller = new SGoodsSizeFiller(subNode, this.xreader);
                XSGoods.Sizes.Add(gsFiller.XSize);
            }
        }
    }
}