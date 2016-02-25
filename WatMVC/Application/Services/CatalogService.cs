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
        public List<Product> GetGoodsByBrandSeriaArticul(string subject_name, string brand_name, string seria_name, string articul)
        {
            var pr = new CatalogRepository();
            // кеш тута
            return pr.GetGoodsByBrandSeriaArticul(subject_name, brand_name, seria_name, articul);
        }

        public List<Product> GetGoodsByBrandSeria(string subject_name, string brand_name, string seria_name)
        {
            var pr = new CatalogRepository();
            // кеш тута
            return pr.GetGoodsByBrandSeria(subject_name, brand_name, seria_name);
        }      

    }
}
