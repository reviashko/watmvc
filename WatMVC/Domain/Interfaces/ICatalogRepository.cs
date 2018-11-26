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
  
        List<int> GetArticulsByMenuId(int menu_id);
        List<Product> GetArticulsByMenuId(byte[] articuls_bin);
    }
}
