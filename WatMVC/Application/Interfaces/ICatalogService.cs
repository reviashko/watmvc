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
    public interface ICatalogService
    {
        List<Product> GetGoodsByBrandSeriaArticul(string subject_name, string brand_name, string seria_name, string articul);
        List<Product> GetGoodsByBrandSeria(string subject_name, string brand_name, string seria_name);

    }
}
