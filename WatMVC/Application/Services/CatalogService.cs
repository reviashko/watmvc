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
    public class CatalogService : ICatalogService
    {
        ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public List<Product> GetGoodsByBrandSeriaArticul(string subject_name, string brand_name, string seria_name, string articul)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByBrandSeriaArticul(subject_name, brand_name, seria_name, articul);
        }

        public List<Product> GetGoodsByBrandSeria(string subject_name, string brand_name, string seria_name)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByBrandSeria(subject_name, brand_name, seria_name);
        }      

    }
}
