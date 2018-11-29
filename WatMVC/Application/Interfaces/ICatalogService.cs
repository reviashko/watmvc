using System;
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
        List<int> GetArticulsByMenuId(int menu_id);
        List<Product> GetGoodsByMenuId(int menu_id, int page_num, int page_size);
        Product GetProductByArticul(int articul, string brand_name);
        List<Product> GetProductsByArticuls(List<int> articuls, int page_num, int page_size);
    }
}
