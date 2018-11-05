using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Xml;
using System.Text;

namespace Domain.Entities
{
    public class PaymentType
    {
        public string PType_id { get; set; }

        public string PType_Name { get; set; }


        public PaymentType()
        {
            PType_id = "";
            PType_Name = "";
        }
    }
}