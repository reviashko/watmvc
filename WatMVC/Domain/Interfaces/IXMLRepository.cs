using System;
using System.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;
using System.Text;

namespace Domain
{
    public interface IXMLRepository
    {
        XmlDocument XmlDoc { get; set; }

        XmlNodeList GetXmlNodes(string nodePath);
        T GetValue<T>(string nodePath, T defaultValue);
        T GetNodeAttribute<T>(XmlNode node, string attributename, T defaultValue);
        T GetAttribute<T>(string attributePath, T defaultValue);
        void LoadXML(string xmlContainer);
    }
}
