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
        List<Product> GetGoodsByCategoryBrandSeriaArticul(int category_id, int brand_id, int seria_id, string articul);
        List<Product> GetGoodsByCategoryBrandSeria(int category_id, int brand_id, int seria_id);
        List<Product> GetGoodsByCategoryBrand(int category_id, int brand_id);        
        List<Product> GetGoodsByCategory(int category_id);
    }
}
