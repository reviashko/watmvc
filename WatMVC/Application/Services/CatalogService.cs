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

        public List<Product> GetGoodsByBrandSeriaArticul(int brand_id, int seria_id, string articul)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByBrandSeriaArticul(brand_id, seria_id, articul);
        }

        public List<Product> GetGoodsByBrandSeria(int brand_id, int seria_id)
        {
            // кеш тута
            return _catalogRepository.GetGoodsByBrandSeria(brand_id, seria_id);
        }      

    }
}
