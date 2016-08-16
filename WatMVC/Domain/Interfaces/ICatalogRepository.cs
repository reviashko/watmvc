using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Domain.Entities;

namespace Domain
{
    public interface ICatalogRepository
    {
        List<Product> GetGoodsByBrandSeriaArticul(int brand_id, int seria_id, string articul);

        List<Product> GetGoodsByBrandSeria(int brand_id, int seria_id);
    }
}
