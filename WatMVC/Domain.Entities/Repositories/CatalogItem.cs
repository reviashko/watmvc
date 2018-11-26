using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class CatalogItem
    {
        public int Menu_id { get; set; }

        public int Link_id { get; set; }

        public CatalogItem()
        {
            Menu_id = 0;
            Link_id = 0;
        }

    }
}