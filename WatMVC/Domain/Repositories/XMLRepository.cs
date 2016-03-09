using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Text;

namespace Domain
{
    public class XMLReader : IXMLRepository
    {
        public XmlDocument XmlDoc { get; set; }

        private XmlElement RootElement;

        public XMLReader()
        {
            XmlDoc = new XmlDocument();
        }

        public void LoadXML(string xmlContainer)
        {
            XmlDoc.LoadXml(xmlContainer);
            RootElement = XmlDoc.DocumentElement;
        }

        public XmlNodeList GetXmlNodes(string nodePath)
        {
            return XmlDoc.DocumentElement.SelectNodes(nodePath);
        }

        public T GetAttribute<T>(string attributeName, T defaultValue)
        {
            if (RootElement.HasAttribute(attributeName))
            {
                string retval = RootElement.GetAttribute(attributeName);
                if (defaultValue is decimal)
                {
                    retval = retval.Replace(".", ",");
                }

                return (T)Convert.ChangeType(retval, typeof(T));
            }

            return defaultValue;
        }

        public T GetNodeAttribute<T>(XmlNode node, string attributename, T defaultValue)
        {
            if (string.IsNullOrEmpty(node.Attributes[attributename].Value))
            {
                return defaultValue;
            }

            string retval = node.Attributes[attributename].Value;
            if (defaultValue is decimal)
            {
                retval = retval.Replace(".", ",");
            }

            return (T)Convert.ChangeType(retval, typeof(T));
        }

        public T GetValue<T>(string nodePath, T defaultValue)
        {
            XmlNodeList nodeList = GetXmlNodes(nodePath);
            if (nodeList != null && nodeList.Count > 0 && nodeList[0].FirstChild != null)
            {
                return (T)Convert.ChangeType(nodeList[0].FirstChild.Value, typeof(T));
            }

            return defaultValue;
        }
    }
}