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
    public class ProductService
    {

        public Product GetGoodsByID(int link_id)
        {
            ProductRepository pr = new ProductRepository();
            // кеш тута
            return pr.GetGoodsByID(link_id);
        }

    }
}
