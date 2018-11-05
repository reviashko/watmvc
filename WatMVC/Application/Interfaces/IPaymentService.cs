﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Domain;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Application
{
    public interface IPaymentService
    {
        List<PaymentType> Get();
        bool IsExists(string value);
    }
}
