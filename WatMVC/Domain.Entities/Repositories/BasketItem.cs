using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class BasketItem
    {
        public int Articul{ get; set; }

        public byte Count { get; set; }

        public BasketItem()
        {
            Articul = 0;
            Count = 0;
        }

    }
}