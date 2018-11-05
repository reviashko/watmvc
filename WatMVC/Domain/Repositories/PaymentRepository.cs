using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using Domain.Entities;

namespace Domain
{
    public class PaymentRepository : IPaymentRepository
    {
        public List<PaymentType> Get()
        {
            IDataBase db = new MSSql();
            db.SetStoredProcedure("MVCWeb.PaymentTypes_Get");
            return db.Query<PaymentType>();
        }
    }
}