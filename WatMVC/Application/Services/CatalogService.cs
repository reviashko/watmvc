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
    public class CatalogService
    {

        public List<Product> GetGoodsBySubject(string subject_name)
        {
            var pr = new CatalogRepository();
            // кеш тута
            return pr.GetGoodsBySubject(subject_name);
        }

        public List<Product> GetGoodsByBrandSubject(string subject_name, string brand_name)
        {
            var pr = new CatalogRepository();
            // кеш тута
            return pr.GetGoodsByBrandSubject(subject_name, brand_name);
        }

        public List<Product> GetGoodsByBrand(string brand_name)
        {
            var pr = new CatalogRepository();
            // кеш тута
            return pr.GetGoodsByBrand(brand_name);
        }        

    }
}
